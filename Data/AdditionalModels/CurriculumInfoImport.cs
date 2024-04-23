using UmkConstructor.Data.DatabaseTables;

namespace UmkConstructor.Data.AdditionalModels
{
    public class CurriculumInfoImport
    {
        public string AcademicYear { get; set; }
        public string Specialty { get; set; }
        public List<Semester> Semesters { get; set; }
    }
}
