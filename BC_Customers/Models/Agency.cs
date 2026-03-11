using BC_Customers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BC_Customers;

public partial class Agency
{
    public Guid Id { get; set; }

    [Column("agency_code")]
    public string AgencyCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? Province { get; set; }

    public string Country { get; set; } = null!;
    [Column("is_active")]
    public bool IsActive { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
