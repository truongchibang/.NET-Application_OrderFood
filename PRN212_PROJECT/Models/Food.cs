using System;
using System.Collections.Generic;

namespace PRN212_PROJECT.Models;

public partial class Food
{
    public int FoodId { get; set; }

    public string FoodName { get; set; } = null!;

    public int TypefId { get; set; }

    public int Price { get; set; }

    public string Image { get; set; } = null!;

    public virtual ICollection<FoodCart> FoodCarts { get; set; } = new List<FoodCart>();

    public virtual ICollection<FoodOrder> FoodOrders { get; set; } = new List<FoodOrder>();

    public virtual TypeFood Typef { get; set; } = null!;
}
