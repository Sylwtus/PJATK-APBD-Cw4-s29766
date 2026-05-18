using Microsoft.EntityFrameworkCore;
using PJATK_APBD_Cw4_s29766.Models;

namespace PJATK_APBD_Cw4_s29766.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<Component> Components { get; set; }
    public DbSet<PCComponent> PCComponents { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }
    public DbSet<PC>  PCs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //klucze
        modelBuilder.Entity<PC>().HasKey(p => p.Id);
        modelBuilder.Entity<Component>().HasKey(c => c.Code);
        modelBuilder.Entity<ComponentManufacturer>().HasKey(cm => cm.Id);
        modelBuilder.Entity<ComponentType>().HasKey(ct => ct.Id);
        modelBuilder.Entity<PCComponent>().HasKey(pc => new {pc.PCId, pc.ComponentCode});
        
        //mapowanie klas na baze danych 

        //relacje
        modelBuilder.Entity<PCComponent>()
            .HasOne(pc => pc.PC)
            .WithMany(p => p.PCComponents)
            .HasForeignKey(pc => pc.PCId);
        modelBuilder.Entity<PCComponent>()
            .HasOne(pc => pc.Component)
            .WithMany(c => c.PCComponents)
            .HasForeignKey(pc => pc.ComponentCode);
        modelBuilder.Entity<Component>()
            .HasOne(c => c.ComponentType)
            .WithMany(t => t.Components)
            .HasForeignKey(cc => cc.ComponentTypeId);
        modelBuilder.Entity<Component>()
            .HasOne(c => c.ComponentManufacturer)
            .WithMany(m => m.Components)
            .HasForeignKey(cc => cc.ComponentManufacturerId);
        
        //ograniczenia pol
        modelBuilder.Entity<PC>()
            .Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();
        modelBuilder.Entity<PC>()
            .Property(p => p.CreatedAt)
            .HasColumnType("datetime");
        modelBuilder.Entity<Component>()
            .Property(c => c.Code)
            .HasColumnType("char(10)")
            .IsRequired();
        modelBuilder.Entity<Component>()
            .Property(c => c.Name)
            .HasMaxLength(300)
            .IsRequired();
        modelBuilder.Entity<Component>()
            .Property(c => c.Description)
            .HasColumnType("nvarchar(max)");
        modelBuilder.Entity<ComponentType>()
            .Property(t => t.Abbreviation)
            .HasMaxLength(30)
            .IsRequired();
        modelBuilder.Entity<ComponentType>()
            .Property(t => t.Name)
            .HasMaxLength(150)
            .IsRequired();
        modelBuilder.Entity<ComponentManufacturer>()
            .Property(cm => cm.FoundationDate)
            .HasColumnType("datetime");
        modelBuilder.Entity<ComponentManufacturer>()
            .Property(cm => cm.Abbreviation)
            .HasMaxLength(30)
            .IsRequired();
        modelBuilder.Entity<ComponentManufacturer>()
            .Property(cm => cm.FullName)
            .HasMaxLength(300)
            .IsRequired();
        
    }
    
}