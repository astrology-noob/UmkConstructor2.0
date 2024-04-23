namespace UmkConstructor.Data.DatabaseTables
{
    public class SemesterCurriculum
    {
        public int RelationId { get; set; }

        public int CurriculumId { get; set; }

        public int SemesterId { get; set; }

        public virtual Curriculum Curriculum { get; set; } = null!;
        
        public virtual Semester Semester { get; set; } = null!;

        public virtual ICollection<DisciplineRealSemester> DisciplineRealSemester { get; set; } = new List<DisciplineRealSemester>();
    }
}
