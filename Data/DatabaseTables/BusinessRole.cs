namespace UmkConstructor.Data.DatabaseTables;

public partial class BusinessRole
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string SearchTag { get; set; } = null!;

    public int SpecialtyId { get; set; }

    public virtual Specialty Specialty { get; set; } = null!;

    public virtual ICollection<Curriculum> Curricula { get; set; } = new List<Curriculum>();
}