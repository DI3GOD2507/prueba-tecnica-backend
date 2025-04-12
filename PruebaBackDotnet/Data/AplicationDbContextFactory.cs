using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PruebaBackDotnet.Models.Entities;

namespace PruebaBackDotnet.Data
{
    public class AplicationDbContextFactory : IDesignTimeDbContextFactory<AplicationDbContext>
    {
        public AplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=prueba;Trusted_Connection=True;TrustServerCertificate=True;");

            return new AplicationDbContext(optionsBuilder.Options);
        }
    }
}
