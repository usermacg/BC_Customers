using System;
using System.Collections.Generic;

namespace BC_Customers.Models;

public partial class Transaction
{
    public Guid Id { get; set; }

    public string TransactionNumber { get; set; } = null!;

    public Guid AccountId { get; set; }

    public int TransactionTypeId { get; set; }

    public Guid EmployeeId { get; set; }

    public Guid AgencyId { get; set; }

    public Guid CashDrawerId { get; set; }

    public decimal Amount { get; set; }

    public decimal BalanceBefore { get; set; }

    public decimal BalanceAfter { get; set; }

    public DateTime TransactionDate { get; set; }

    public string Status { get; set; } = null!;

    public string? Description { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Agency Agency { get; set; } = null!;

    public virtual CashDrawer CashDrawer { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<TransactionReversal> TransactionReversals { get; set; } = new List<TransactionReversal>();

    public virtual TransactionType TransactionType { get; set; } = null!;
}
