using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WarehouseManagement.Models;

public partial class OutputInfo
{
    public int Id { get; set; }

    public int IdObject { get; set; }

    public int IdOutputInfo { get; set; }

    public int IdCustomer { get; set; }

    public int? Count { get; set; }

    public string? Status { get; set; }

    public double? PriceOutput { get; set; }

    public virtual Customer IdCustomerNavigation { get; set; } = null!;

    public virtual Object IdObjectNavigation { get; set; } = null!;

    public virtual Output IdOutputInfoNavigation { get; set; } = null!;
}
