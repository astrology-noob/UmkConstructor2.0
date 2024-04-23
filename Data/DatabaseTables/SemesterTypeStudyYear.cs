namespace UmkConstructor.Data.DatabaseTables;

public partial class SemesterTypeStudyYear
{
    public int RelationId { get; set; }

    public int SemesterTypeId { get; set; }

    public int StudyYearId { get; set; }

    public virtual ICollection<Semester> Semesters { get; set; } = new List<Semester>();

    public virtual SemesterType SemesterType { get; set; } = null!;

    public virtual StudyYear StudyYear { get; set; } = null!;
}
