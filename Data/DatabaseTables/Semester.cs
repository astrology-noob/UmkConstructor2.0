namespace UmkConstructor.Data.DatabaseTables;

public partial class Semester
{
    public int Id { get; set; }

    public int SemesterTypeStudyYearId { get; set; }

    public int HoursTotal { get; set; }

    public int IndividualWork { get; set; }

    public int WeekCount { get; set; }

    public int EduPracticeWeekCount { get; set; }

    public int ProdPracticeWeekCount { get; set; }

    public int SessionWeekCount { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual SemesterTypeStudyYear SemesterTypeStudyYear { get; set; } = null!;

    public virtual ICollection<SemesterCurriculum> SemesterCurriculum { get; set; } = new List<SemesterCurriculum>();
}