using UmkConstructor.Data.DatabaseTables;

namespace UmkConstructor.Data.AdditionalModels
{
    public class FullCurriculumInfo
    {
        public AcademicYear AcademicYear { get; set; }
        // через бизнес-роль получаю инфу о специальности и соответственно о кафедре и организации
        public BusinessRole BusinessRole { get; set; }

        public List<FullSemesterInfo> Semesters { get; set; }
    }
}
