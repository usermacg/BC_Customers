using System;
using System.Collections.Generic;

namespace BC_Customers.Models;

public partial class CashDrawer
{
    public Guid Id { get; set; }

    public Guid EmployeeId { get; set; }

    public decimal OpeningBalance { get; set; }

    public decimal CurrentBalance { get; set; }

    public DateTime OpenedAt { get; set; }

    public DateTime? ClosedAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
