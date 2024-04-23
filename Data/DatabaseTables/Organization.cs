namespace UmkConstructor.Data.DatabaseTables;

public partial class Organization
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TemplateOrganization> TemplateOrganization { get; set; } = new List<TemplateOrganization>();
}
