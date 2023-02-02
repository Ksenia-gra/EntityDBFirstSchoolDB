using System;
using System.Collections.Generic;

namespace Lab2_SchoolDataBase;

public partial class Class
{
    public long Id { get; set; }

    public long? ClassMainteacherId { get; set; }

    public long? KindId { get; set; }

    public long? StudentsCount { get; set; }

    public string ClassLetter { get; set; } = null!;

    public long? StudyYear { get; set; }

    public long? CreateYear { get; set; }

    public virtual KindsOfClass? Kind { get; set; }

    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
