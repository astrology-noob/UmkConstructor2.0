using System;
using System.Collections.Generic;

namespace UmkConstructor.Data.DatabaseTables;

public partial class StudyYear
{
    public int Id { get; set; }

    public int Order { get; set; }

    public bool IsAfter11thGrade { get; set; }

    public virtual ICollection<SemesterTypeStudyYear> SemesterTypeStudyYear { get; set; } = new List<SemesterTypeStudyYear>();
}
