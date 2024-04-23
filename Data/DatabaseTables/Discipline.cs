using UmkConstructor.Data.AdditionalModels;

namespace UmkConstructor.Data.DatabaseTables
{
    public class Discipline
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Hours { get; set; }

        public bool IsDeprecated { get; set; }

        public Code Code { get; set; }

        public virtual ICollection<DisciplineRealSemester> DisciplineRealSemester { get; set; } = new List<DisciplineRealSemester>();
    }
}
