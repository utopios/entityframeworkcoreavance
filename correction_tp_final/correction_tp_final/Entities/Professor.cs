using System;
using System.Collections.Generic;

namespace correction_tp_final.Entities;

public partial class Professor
{
    public int ProfessorId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public DateOnly HireDate { get; set; }

    public decimal Salary { get; set; }

    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;
}
