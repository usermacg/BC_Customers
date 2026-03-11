using System;
using System.Collections.Generic;
using BC_Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace BC_Customers.Services;

public partial class BcCustomersDbContext : DbContext
{
    public BcCustomersDbContext()
    {
    }

    public BcCustomersDbContext(DbContextOptions<BcCustomersDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Agency> Agencies { get; set; }

    public virtual DbSet<CashDrawer> CashDrawers { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TransactionReversal> TransactionReversals { get; set; }

    public virtual DbSet<TransactionType> TransactionTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BC_Customers_db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("accounts");

            entity.HasIndex(e => e.AccountNumber, "UQ_accounts_number").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("id");
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("account_number");
            entity.Property(e => e.AccountType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("account_type");
            entity.Property(e => e.Balance)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("balance");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("currency");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("active")
                .HasColumnName("status");

            entity.HasOne(d => d.Client).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_accounts_client");
        });

        modelBuilder.Entity<Agency>(entity =>
        {
            entity.ToTable("agencies");

            entity.HasIndex(e => e.AgencyCode, "UQ_agencies_code").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.AgencyCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("agency_code");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasDefaultValue("RDC")
                .HasColumnName("country");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Province)
                .HasMaxLength(100)
                .HasColumnName("province");
        });

        modelBuilder.Entity<CashDrawer>(entity =>
        {
            entity.ToTable("cash_drawers");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("id");
            entity.Property(e => e.ClosedAt).HasColumnName("closed_at");
            entity.Property(e => e.CurrentBalance)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("current_balance");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.OpenedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("opened_at");
            entity.Property(e => e.OpeningBalance)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("opening_balance");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("open")
                .HasColumnName("status");

            entity.HasOne(d => d.Employee).WithMany(p => p.CashDrawers)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cashdrawer_employee");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("clients", tb => tb.HasTrigger("TRG_clients_updated_at"));

            entity.HasIndex(e => e.DeletedAt, "IX_clients_deleted_at");

            entity.HasIndex(e => e.Email, "IX_clients_email");

            entity.HasIndex(e => e.KycStatus, "IX_clients_kyc_status");

            entity.HasIndex(e => e.PhoneNumber, "IX_clients_phone");

            entity.HasIndex(e => e.RiskProfile, "IX_clients_risk_profile");

            entity.HasIndex(e => e.Status, "IX_clients_status");

            entity.HasIndex(e => e.CustomerNumber, "UQ_clients_customer_number").IsUnique();

            entity.HasIndex(e => new { e.IdentityType, e.IdentityNumber }, "UQ_clients_identity").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("id");
            entity.Property(e => e.AccountLockedUntil).HasColumnName("account_locked_until");
            entity.Property(e => e.AddressLine1)
                .HasMaxLength(255)
                .HasColumnName("address_line_1");
            entity.Property(e => e.AddressLine2)
                .HasMaxLength(255)
                .HasColumnName("address_line_2");
            entity.Property(e => e.AddressVerified).HasColumnName("address_verified");
            entity.Property(e => e.AnnualIncome)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("annual_income");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.ComplianceNotes).HasColumnName("compliance_notes");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())", "DF_clients_created_at")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CrsStatus).HasColumnName("crs_status");
            entity.Property(e => e.CustomerNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("customer_number");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.EmployerName)
                .HasMaxLength(255)
                .HasColumnName("employer_name");
            entity.Property(e => e.EmploymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("employment_status");
            entity.Property(e => e.FailedLoginAttempts).HasColumnName("failed_login_attempts");
            entity.Property(e => e.FatcaStatus).HasColumnName("fatca_status");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.GdprConsent).HasColumnName("gdpr_consent");
            entity.Property(e => e.GdprConsentAt).HasColumnName("gdpr_consent_at");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.IdentityExpiryDate).HasColumnName("identity_expiry_date");
            entity.Property(e => e.IdentityIssuedDate).HasColumnName("identity_issued_date");
            entity.Property(e => e.IdentityNumber)
                .HasMaxLength(150)
                .HasColumnName("identity_number");
            entity.Property(e => e.IdentityType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("identity_type");
            entity.Property(e => e.IdentityVerified).HasColumnName("identity_verified");
            entity.Property(e => e.IncomeCurrency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("income_currency");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(255)
                .HasColumnName("job_title");
            entity.Property(e => e.KycStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("pending", "DF_clients_kyc_status")
                .HasColumnName("kyc_status");
            entity.Property(e => e.KycVerifiedAt).HasColumnName("kyc_verified_at");
            entity.Property(e => e.KycVerifiedBy).HasColumnName("kyc_verified_by");
            entity.Property(e => e.LastLoginAt).HasColumnName("last_login_at");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("marital_status");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(100)
                .HasColumnName("middle_name");
            entity.Property(e => e.Nationality)
                .HasMaxLength(100)
                .HasColumnName("nationality");
            entity.Property(e => e.NumberOfDependents).HasColumnName("number_of_dependents");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(500)
                .HasColumnName("password_hash");
            entity.Property(e => e.PepStatus).HasColumnName("pep_status");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .HasColumnName("postal_code");
            entity.Property(e => e.PreferredContactMethod)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("preferred_contact_method");
            entity.Property(e => e.RiskProfile)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("medium", "DF_clients_risk_profile")
                .HasColumnName("risk_profile");
            entity.Property(e => e.SanctionsCheckStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("pending", "DF_clients_sanctions")
                .HasColumnName("sanctions_check_status");
            entity.Property(e => e.SecondaryPhone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("secondary_phone");
            entity.Property(e => e.SourceOfFunds)
                .HasMaxLength(255)
                .HasColumnName("source_of_funds");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .HasColumnName("state");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("pending", "DF_clients_status")
                .HasColumnName("status");
            entity.Property(e => e.TwoFactorEnabled).HasColumnName("two_factor_enabled");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(sysdatetime())", "DF_clients_updated_at")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("employees");

            entity.HasIndex(e => e.EmployeeCode, "UQ_employees_code").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("id");
            entity.Property(e => e.AgencyId).HasColumnName("agency_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.EmployeeCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("employee_code");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("role");

            entity.HasOne(d => d.Agency).WithMany(p => p.Employees)
                .HasForeignKey(d => d.AgencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_employees_agency");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("transactions");

            entity.HasIndex(e => e.TransactionNumber, "UQ_transactions_number").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.AgencyId).HasColumnName("agency_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.BalanceAfter)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("balance_after");
            entity.Property(e => e.BalanceBefore)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("balance_before");
            entity.Property(e => e.CashDrawerId).HasColumnName("cash_drawer_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("completed")
                .HasColumnName("status");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("transaction_date");
            entity.Property(e => e.TransactionNumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("transaction_number");
            entity.Property(e => e.TransactionTypeId).HasColumnName("transaction_type_id");

            entity.HasOne(d => d.Account).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_transactions_account");

            entity.HasOne(d => d.Agency).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.AgencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_transactions_agency");

            entity.HasOne(d => d.CashDrawer).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CashDrawerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_transactions_drawer");

            entity.HasOne(d => d.Employee).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_transactions_employee");

            entity.HasOne(d => d.TransactionType).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.TransactionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_transactions_type");
        });

        modelBuilder.Entity<TransactionReversal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__transact__3213E83F853F7829");

            entity.ToTable("transaction_reversals");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("id");
            entity.Property(e => e.OriginalTransactionId).HasColumnName("original_transaction_id");
            entity.Property(e => e.ReversalReason)
                .HasMaxLength(255)
                .HasColumnName("reversal_reason");
            entity.Property(e => e.ReversedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("reversed_at");
            entity.Property(e => e.ReversedBy).HasColumnName("reversed_by");

            entity.HasOne(d => d.OriginalTransaction).WithMany(p => p.TransactionReversals)
                .HasForeignKey(d => d.OriginalTransactionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_reversal_transaction");

            entity.HasOne(d => d.ReversedByNavigation).WithMany(p => p.TransactionReversals)
                .HasForeignKey(d => d.ReversedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_reversal_employee");
        });

        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.ToTable("transaction_types");

            entity.HasIndex(e => e.Code, "UQ_transaction_types_code").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .HasColumnName("description");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
