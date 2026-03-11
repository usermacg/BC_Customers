using System;
using System.Collections.Generic;

namespace BC_Customers.Models;

public partial class Client
{
    public Guid Id { get; set; }

    public string CustomerNumber { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string Nationality { get; set; } = null!;

    public string? MaritalStatus { get; set; }

    public int NumberOfDependents { get; set; }

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? SecondaryPhone { get; set; }

    public string? PreferredContactMethod { get; set; }

    public string AddressLine1 { get; set; } = null!;

    public string? AddressLine2 { get; set; }

    public string City { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public string? State { get; set; }

    public string Country { get; set; } = null!;

    public bool AddressVerified { get; set; }

    public string IdentityType { get; set; } = null!;

    public string IdentityNumber { get; set; } = null!;

    public DateOnly? IdentityIssuedDate { get; set; }

    public DateOnly? IdentityExpiryDate { get; set; }

    public bool IdentityVerified { get; set; }

    public string KycStatus { get; set; } = null!;

    public DateTime? KycVerifiedAt { get; set; }

    public Guid? KycVerifiedBy { get; set; }

    public string? EmploymentStatus { get; set; }

    public string? EmployerName { get; set; }

    public string? JobTitle { get; set; }

    public decimal? AnnualIncome { get; set; }

    public string? IncomeCurrency { get; set; }

    public string RiskProfile { get; set; } = null!;

    public bool PepStatus { get; set; }

    public string SanctionsCheckStatus { get; set; } = null!;

    public bool FatcaStatus { get; set; }

    public bool CrsStatus { get; set; }

    public string? SourceOfFunds { get; set; }

    public string PasswordHash { get; set; } = null!;

    public bool TwoFactorEnabled { get; set; }

    public int FailedLoginAttempts { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public DateTime? AccountLockedUntil { get; set; }

    public string? ComplianceNotes { get; set; }

    public bool GdprConsent { get; set; }

    public DateTime? GdprConsentAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
