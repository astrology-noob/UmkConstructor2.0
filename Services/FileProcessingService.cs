using Microsoft.AspNetCore.Mvc;
using UmkConstructor.Data.AdditionalModels;
using UmkConstructor.Data.ParsingClasses;

namespace UmkConstructor.Services
{
    public class FileProcessingService
    {
        FileParser processor = new();

        private readonly CurriculaService CS;

        // для того чтобы использовать здесь Curricula Service
        // это вообще заработает??
        public FileProcessingService(CurriculaService cs)
        {
            CS = cs;
        }

        // почему-то переходит не по этому адресу а просто по CreateCurriculumWithFile
        // может вернуть List<ParsingError> с ошибками чтения файла
        // или
        // List<SingleSemesterParsingResult> с семестрами, в парсинге которых возникли критические ошибки
        public async Task<List<SingleSemesterParsingResult>> CreateCurriculumWithFile(IFormFileCollection formFiles, string specialty, string academicYear)
        {
            SemestersParsingResult result = processor.ParseCurriculumFile(formFiles);

            // как вернуть ошибку чтения файла?, если я возвращаю список отпаршенных семестров с ошибками?
            // возвращать только ошибки я не смогу, потому что иначе у меня потеряется связь с источниками ошибок
            // возможно возвращать словарь с ключами-источниками и значениями списками с ошибками?

            // здесь нужно брать только семестры в которых нет критических ошибок, то есть которые successful = true

            List<SemesterInfoImport> semestersGood = new List<SemesterInfoImport>();
            semestersGood = result.Results.Where(x => x.Successful).Select(x => new SemesterInfoImport(x)).ToList();

            // добавить проверку, является файл действительно учебным планом для выбранной специальности (то есть если бизнес-роли не относятся к специальности, то забить
            CurriculumInfoImport newCurriculum = new CurriculumInfoImport()
            {
                Specialty = specialty,
                AcademicYear = academicYear,
                Semesters = semestersGood
            };

            await CS.AddCurriculumFromParsedEntity(newCurriculum);

            // возвращаем ошибки
            List<SingleSemesterParsingResult> semestersErrors = new List<SingleSemesterParsingResult>();
            semestersErrors = result.Results.Where(x => !x.Successful).Select(x => x).ToList();

            return semestersErrors;
        }
    }
}
