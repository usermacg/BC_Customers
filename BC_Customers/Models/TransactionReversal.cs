using System;
using System.Collections.Generic;

namespace BC_Customers.Models;

public partial class TransactionReversal
{
    public Guid Id { get; set; }

    public Guid OriginalTransactionId { get; set; }

    public Guid ReversedBy { get; set; }

    public string ReversalReason { get; set; } = null!;

    public DateTime? ReversedAt { get; set; }

    public virtual Transaction OriginalTransaction { get; set; } = null!;

    public virtual Employee ReversedByNavigation { get; set; } = null!;
}
