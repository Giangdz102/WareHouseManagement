using System;
using System.Collections.Generic;

namespace WarehouseManagement.Models;

public partial class InputInfo
{
    public int Id { get; set; }

    public int IdObject { get; set; }

    public int IdInput { get; set; }

    public int? Count { get; set; }

    public double? InputPrice { get; set; }

    public double? OutputPrice { get; set; }

    public string? Status { get; set; }

    public virtual Input IdInputNavigation { get; set; } = null!;

    public virtual Object IdObjectNavigation { get; set; } = null!;

}
