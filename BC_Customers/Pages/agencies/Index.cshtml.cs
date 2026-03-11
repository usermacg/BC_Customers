using BC_Customers.Pages.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using BC_Customers.Services;
using BC_Customers.Models;

namespace BC_Customers.Pages.agencies
{
    public class IndexModel : PageModel
    {
        private readonly BcCustomersDbContext contexte;
        public List<Agency> listAgencies = new();
        public List<Agency> listAgencies1 = new();

        public IndexModel(BcCustomersDbContext contexte) {
            this.contexte = contexte;
        }

        public void OnGet()
        {
            try
            {
               /*String ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BC_Customers_db;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                SqlConnection connection = new SqlConnection(ConnectionString);
                
                connection.Open();
                String sql = "SELECT [id] ,[agency_code],[name] ,[address] ,[city] ,[province] ,[country] ,[is_active] ,[created_at]  FROM [BC_Customers_db].[dbo].[agencies] ";
                SqlCommand command = new SqlCommand(sql, connection);

                SqlDataReader reader = command.ExecuteReader();
                        
                while (reader.Read())
                {
                    Agency agency = new Agency
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("id")),
                        AgencyCode = reader.GetString(reader.GetOrdinal("agency_code")),
                        Name = reader.GetString(reader.GetOrdinal("name")),
                        Address = reader.GetString(reader.GetOrdinal("address")),
                        City = reader.GetString(reader.GetOrdinal("city")),
                        Province = reader.GetString(reader.GetOrdinal("province")),
                        Country = reader.GetString(reader.GetOrdinal("country")),
                        IsActive = reader.GetString(reader.GetOrdinal("is_active")),
                        CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at"))
                    };

                    listAgencies.Add(agency);
                }
                connection.Close();*/
               listAgencies1 = contexte.Agencies.ToList();
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    
    }
    public class AgenciesInfo
    {
        [Key]
        public Guid Id;

        [Required]
        [StringLength(10)]
        public required string AgencyCode { get; set; }

        [Required]
        [StringLength(150)]
        public required string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string? Address { get; set; }

        [Required]
        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? Province { get; set; }

        [Required]
        [StringLength(100)]
        public string? Country { get; set; } 

        [Required]
        public required string IsActive { get; set; } 

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
