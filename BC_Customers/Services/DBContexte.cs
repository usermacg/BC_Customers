using BC_Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace BC_Customers.Services
{
    public class DBContexte:DbContext
    {
        public DBContexte(DbContextOptions option) : base(option) { 
        
        }

        public DbSet<Agency> Agencies { get; set; } = null;
    }
}
