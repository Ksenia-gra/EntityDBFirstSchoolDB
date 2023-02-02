using System;
using System.Collections.Generic;

namespace Lab2_SchoolDataBase;

public partial class Student
{
    public long Id { get; set; }

    public string Fio { get; set; } = null!;

    public string DateOfBirth { get; set; } = null!;

    public string? Gender { get; set; }

    public string? Adress { get; set; }

    public string? FatherFio { get; set; }

    public string? MotherFio { get; set; }

    public long? ClassId { get; set; }

    public string? AdditInfo { get; set; }

    public virtual Class? Class { get; set; }
}
