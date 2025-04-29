using System;
using System.Collections.Generic;

namespace PRN212_PROJECT.Models;

public partial class FoodCart
{
    public int FoodCart1 { get; set; }

    public int CartId { get; set; }

    public int FoodId { get; set; }

    public int Quantity { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Food Food { get; set; } = null!;
}
