namespace UmkConstructor.Data.DatabaseTables;

public partial class Specialty
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<BusinessRole> BusinessRoles { get; set; } = new List<BusinessRole>();
}
