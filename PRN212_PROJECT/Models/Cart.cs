using System;
using System.Collections.Generic;

namespace PRN212_PROJECT.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public string AccountId { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<FoodCart> FoodCarts { get; set; } = new List<FoodCart>();
}
