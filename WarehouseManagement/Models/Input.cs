using System;
using System.Collections.Generic;

namespace WarehouseManagement.Models;

public partial class Input
{
    public int Id { get; set; }

    public DateTime? DateInput { get; set; }

    public virtual ICollection<InputInfo> InputInfos { get; set; } = new List<InputInfo>();
}
