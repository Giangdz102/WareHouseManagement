using System;
using System.Collections.Generic;

namespace WarehouseManagement.Models;

public partial class Output
{
    public string Id { get; set; } = null!;

    public DateTime? DateOutput { get; set; }
}
