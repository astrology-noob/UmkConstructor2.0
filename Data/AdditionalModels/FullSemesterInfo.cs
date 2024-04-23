using UmkConstructor.Data.DatabaseTables;

namespace UmkConstructor.Data.AdditionalModels
{
    public class FullSemesterInfo
    {
        public Semester Semester { get; set; }
        public SemesterTypeStudyYear SemesterTypeStudyYear { get; set; }
        public List<Discipline> Disciplines { get; set; }
    }
}
