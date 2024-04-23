namespace UmkConstructor.Data.DatabaseTables;

public partial class SemesterType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<SemesterTypeStudyYear> SemesterTypeStudyYear { get; set; } = new List<SemesterTypeStudyYear>();
}
