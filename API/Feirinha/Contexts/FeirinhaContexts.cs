using Feirinha.Domains;
using Microsoft.EntityFrameworkCore;

namespace Feirinha.Contexts
{
    public class FeirinhaContexts : DbContext
    {

        public FeirinhaContexts(DbContextOptions<FeirinhaContexts> options) : base(options) { }

        public virtual DbSet<Products> Products { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Data Source=NOTE01-S21; initial catalog=Feirinha; user Id = sa; pwd=Senai@134; TrustServerCertificate=true");
        //    }
        //}
    }
}
