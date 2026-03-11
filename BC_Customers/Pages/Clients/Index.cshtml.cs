using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Security.Principal;

namespace BC_Customers.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<clientsInfo> listClients = new List<clientsInfo>();
        public void OnGet()
        {
            try
            {
                String ConnectionString = "Data Source=.\\MSSQLLocalDB;Initial Catalog=BC_Customers_db;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM  clients";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read()) {
                                clientsInfo clientInfo = new clientsInfo
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    CustomerNumber = reader.GetString(20),
                                    FirstName = reader.GetString(30),
                                    LastName = reader.GetString(31),
                                    Email = reader.GetString(32),
                                    Address = reader.GetString(32),
                                    PhoneNumber = reader.GetString(12),
                                    City = reader.GetString(32),
                                    Province = reader.GetString(32),
                                    Country = reader.GetString(32),
                                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                                };

                                listClients.Add(clientInfo);

                                 }
                            }
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public class clientsInfo
    {
        [Key]
        public Guid Id { get; set; }  // UNIQUEIDENTIFIER

        [Required]
        [StringLength(20)]
        public required string CustomerNumber { get; set; }  // customer_number, UNIQUE

        [Required]
        [StringLength(100)]
        [Display(Name = "Prénom")]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nom")]
        public string? LastName { get; set; }

        [StringLength(150)]
        [EmailAddress]
        public required string Email { get; set; }

        [StringLength(20)]
        [Phone]
        [Display(Name = "Téléphone")]
        public required string PhoneNumber { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? Province { get; set; }

        [StringLength(100)]
        public string Country { get; set; } = "RDC";

        [Display(Name = "Date de création")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property (optionnel, si tu veux lier les comptes)
       // public ICollection<Account> Accounts { get; set; }
    }
}
