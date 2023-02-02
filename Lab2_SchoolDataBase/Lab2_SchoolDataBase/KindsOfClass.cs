using System;
using System.Collections.Generic;

namespace Lab2_SchoolDataBase;

public partial class KindsOfClass
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Class> Classes { get; } = new List<Class>();
}
