using Ajmera_Core.Builders;
using Ajmera_Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ajmera_Data.Data
{
    public partial class AjmeraDbContext : DbContext
    {
        public AjmeraDbContext(DbContextOptions<AjmeraDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new BookBuilder(modelBuilder.Entity<Book>());
        }
    }
}
