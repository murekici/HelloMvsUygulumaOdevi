using HelloMvs.Models;
using Microsoft.EntityFrameworkCore;
namespace HelloMvs.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        { 
        
        }   
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-E77AB2OJ; Initial Catalog=OdevDB;Integrated Security=true;TrustServerCertificate=true");
        }
        public DbSet<Ogrenci>Ogrenciler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ogrenci>().ToTable("tblOgrenciler");
            modelBuilder.Entity<Ogrenci>().Property(o => o.Ad).HasColumnType("varchar").HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Ogrenci>().Property(o=>o.Soyad).HasColumnType("varchar").HasMaxLength(40).IsRequired();
        }
    }
}
