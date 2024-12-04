﻿using System;
using System.Collections.Generic;

namespace correction_tp_final.Entities;

public partial class StudentCourse
{
    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public decimal? Grade { get; set; }

    public DateTime EnrollmentDate { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
