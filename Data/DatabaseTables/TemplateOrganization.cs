namespace UmkConstructor.Data.DatabaseTables
{
    public class TemplateOrganization
    {
        public int RelationId { get; set; }

        public int TemplateId { get; set; }

        public int OrganizationId { get; set; }

        public virtual Template Template { get; set; } = null!;

        public virtual Organization Organization { get; set; } = null!;
    }
}
