using UmkConstructor.Data.AdditionalModels;

namespace UmkConstructor.Data.DatabaseTables
{
    public class Template
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string FileName { get; set; } = null!;

        public Edition Edition { get; set; }

        public bool IsDeprecated { get; set; }

        public virtual ICollection<TemplateOrganization> TemplateOrganization { get; set; } = new List<TemplateOrganization>();
    }
}
