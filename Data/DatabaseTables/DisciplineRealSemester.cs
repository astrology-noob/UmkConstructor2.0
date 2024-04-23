namespace UmkConstructor.Data.DatabaseTables
{
    public class DisciplineRealSemester
    {
        public int RelationId { get; set; }

        public int DisciplineId { get; set; }

        public int SemesterCurriculumId { get; set; }

        public virtual Discipline Discipline { get; set; } = null!;

        public virtual SemesterCurriculum SemesterCurriculum { get; set; } = null!;
    }
}
