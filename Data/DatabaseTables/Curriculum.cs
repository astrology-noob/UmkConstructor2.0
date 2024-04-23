using UmkConstructor.Data.AdditionalModels;
namespace UmkConstructor.Data.DatabaseTables;

public partial class Curriculum
{
    public int Id { get; set; }

    public int AcademicYearId { get; set; }

    public int BusinessRoleId { get; set; }

    public Edition Edition { get; set; }

    public bool IsDeprecated { get; set; }

    public virtual AcademicYear AcademicYear { get; set; } = null!;

    public virtual BusinessRole BusinessRole { get; set; } = null!;

    public virtual ICollection<SemesterCurriculum> SemesterCurriculum { get; set; } = new List<SemesterCurriculum>();
}
