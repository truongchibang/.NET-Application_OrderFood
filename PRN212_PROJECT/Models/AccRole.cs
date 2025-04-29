using System;
using System.Collections.Generic;

namespace PRN212_PROJECT.Models;

public partial class AccRole
{
    public string AccountId { get; set; } = null!;

    public int RoleId { get; set; }

    public string? Address { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
