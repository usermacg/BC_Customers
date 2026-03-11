using System;
using System.Collections.Generic;

namespace BC_Customers.Models;

public partial class Employee
{
    public Guid Id { get; set; }

    public Guid AgencyId { get; set; }

    public string EmployeeCode { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Role { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Agency Agency { get; set; } = null!;

    public virtual ICollection<CashDrawer> CashDrawers { get; set; } = new List<CashDrawer>();

    public virtual ICollection<TransactionReversal> TransactionReversals { get; set; } = new List<TransactionReversal>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
