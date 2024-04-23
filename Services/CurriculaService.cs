using Microsoft.EntityFrameworkCore;
using UmkConstructor.Data;
using UmkConstructor.Data.AdditionalModels;
using UmkConstructor.Data.DatabaseTables;

namespace UmkConstructor.Services
{
    public class CurriculaService
    {
        private ApplicationDbContext _dbContext;

        public CurriculaService(ApplicationDbContext db)
        {
            _dbContext = db;
        }

        public async Task<FullCurriculumInfo> GetFullInfoOfCurriculumById(int id)
        {
            var result = await _dbContext.Curricula
                .Include(x => x.BusinessRole)
                .Include(x => x.AcademicYear)
                // разобраться почему не работает Edition
                //.Include(x => x.Edition)
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (result is null)
            {
                //делать что-то если он нифига не нашёл
            }

            FullCurriculumInfo fullCurriculumInfo = new();
            fullCurriculumInfo.AcademicYear = result!.AcademicYear;
            fullCurriculumInfo.BusinessRole = result!.BusinessRole;

            // здесь я получается лишний раз получаю информацию по учебным планам или нет?
            var semesters = (from semester in _dbContext.Semesters
                            join semesterTypeStudyYear in _dbContext.SemesterTypeStudyYear on semester.SemesterTypeStudyYearId equals semesterTypeStudyYear.RelationId
                            join semesterCurriculum in _dbContext.SemesterCurriculum on semester.Id equals semesterCurriculum.SemesterId
                            join curricula in _dbContext.Curricula on semesterCurriculum.CurriculumId equals curricula.Id
                            where curricula.Id == id
                            select
                            new FullSemesterInfo
                            {
                                Semester = semester,
                                SemesterTypeStudyYear = semesterTypeStudyYear
                            }).ToList();
            
            List<Discipline> disciplines;
            fullCurriculumInfo.Semesters = new();

            foreach (var semesterInfo in semesters)
            {
                disciplines = (from discipline in _dbContext.Disciplines
                               join disciplineSemester in _dbContext.DisciplineRealSemester on discipline.Id equals disciplineSemester.DisciplineId
                               join semesterCurriculum in _dbContext.SemesterCurriculum on disciplineSemester.SemesterCurriculumId equals semesterCurriculum.RelationId
                               join semesterz in _dbContext.Semesters on semesterCurriculum.SemesterId equals semesterz.Id
                               where semesterz.Id == semesterInfo.Semester.Id
                               select discipline
                               ).ToList();

                semesterInfo.Disciplines = disciplines;

                fullCurriculumInfo.Semesters.Add(semesterInfo);
            }

            return fullCurriculumInfo;
        }

        public async Task<List<Curriculum>> GetCurriculaFromDbAsync() => await _dbContext.Curricula.Include(x => x.AcademicYear).Include(x => x.BusinessRole).ToListAsync();

        public async Task<List<Specialty>> GetAllSpecialties() => await _dbContext.Specialties.ToListAsync();

        public async Task<List<StudyYear>> GetStudyYears(bool isAfter11thGrade = false) => await _dbContext.StudyYears.Where(x => x.IsAfter11thGrade == isAfter11thGrade).ToListAsync();
        
        public async Task<List<SemesterType>> GetAllSemesterTypes() => await _dbContext.SemesterTypes.ToListAsync();

        public async Task<SemesterTypeStudyYear> GetSemesterTypeStudyYearByProps(SemesterType semesterType, StudyYear studyYear) => 
            await _dbContext.SemesterTypeStudyYear
            .Where(x => x.SemesterTypeId == semesterType.Id)
            .Where(x => x.StudyYearId == studyYear.Id)
            .FirstOrDefaultAsync();

        public async Task<List<AcademicYear>> GetAllAcademicYears() => await _dbContext.AcademicYears.ToListAsync();

        // ПОКА НИЧЕГО НЕ СОХРАНЯЕТ В БД
        public async Task AddCurriculumFromParsedEntity(CurriculumInfoImport parsedCurriculum)
        {
            await new Task(() => Console.WriteLine("типа добавила учебный план"));
        }

        
        public async Task AddSemesterFromForm(int curriculumId, Semester newSemester, SemesterTypeStudyYear semesterTypeStudyYear)
        {
            await _dbContext.Semesters.AddAsync(newSemester);
            
            // бесполезная проверка, такого быть не должно, чтобы не было нужного типа семестра
            //if (semesterTypeStudyYear.RelationId == 0)
            //{
            //    await _dbContext.SemesterTypeStudyYear.AddAsync(semesterTypeStudyYear);
            //}

            newSemester.SemesterTypeStudyYear = semesterTypeStudyYear;

            // добавляем запись для таблицы многие ко многим
            // проверить, возможно ли так делать
            await _dbContext.SemesterCurriculum.AddAsync(new()
            {
                Semester = newSemester,
                Curriculum = await _dbContext.Curricula.FirstOrDefaultAsync(x => x.Id == curriculumId)
            });

            await _dbContext.SaveChangesAsync();
            // в 
            // не забыть создавать связь в semetsercurriculum
        }

        public async Task AddExistingDisciplineToSemester()
        {

        }

        public async Task AddNewDisciplineToSemester()
        {

        }
    }
}
