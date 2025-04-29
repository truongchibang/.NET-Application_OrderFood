using System;
using System.Collections.Generic;

namespace PRN212_PROJECT.Models;

public partial class FoodOrder
{
    public int FoodOrder1 { get; set; }

    public int OrderId { get; set; }

    public int FoodId { get; set; }

    public int Quantity { get; set; }

    public virtual Food Food { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
