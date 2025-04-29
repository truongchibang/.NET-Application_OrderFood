using System;
using System.Collections.Generic;

namespace PRN212_PROJECT.Models;

public partial class TypeFood
{
    public int TypefId { get; set; }

    public string Typename { get; set; } = null!;

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();
}
