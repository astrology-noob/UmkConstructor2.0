using System;
using System.Collections.Generic;

namespace UmkConstructor.Data.DatabaseTables;

// кафедра
public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}
