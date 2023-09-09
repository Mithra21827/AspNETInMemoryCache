using System;
using System.Collections.Generic;

namespace Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? Name { get; set; }

    public int Age { get; set; }

    public string? Department { get; set; }
}
