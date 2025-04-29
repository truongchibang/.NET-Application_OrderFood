using System;
using System.Collections.Generic;

namespace PRN212_PROJECT.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string AccountId { get; set; } = null!;

    public int Status { get; set; }

    public int Rate { get; set; }
    public string? Comment { get; set; }
    public DateTime? TimeOder { get; set; }

    public DateTime? TimeFinish { get; set; }

    public int Total { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<FoodOrder> FoodOrders { get; set; } = new List<FoodOrder>();

    public virtual Status StatusNavigation { get; set; } = null!;
}
