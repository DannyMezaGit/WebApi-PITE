using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Alumn> Alumns { get; set; }

    }
}