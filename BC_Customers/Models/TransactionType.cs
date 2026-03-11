using System;
using System.Collections.Generic;

namespace BC_Customers.Models;

public partial class TransactionType
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
