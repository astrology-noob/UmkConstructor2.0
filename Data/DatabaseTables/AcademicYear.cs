namespace UmkConstructor.Data.DatabaseTables;

public partial class AcademicYear
{
    public int Id { get; set; }

    public int Start { get; set; }

    public int End { get; set; }

    public virtual ICollection<Curriculum> Curricula { get; set; } = new List<Curriculum>();

    public override string ToString() => string.Concat(Start, "-", End);
}
