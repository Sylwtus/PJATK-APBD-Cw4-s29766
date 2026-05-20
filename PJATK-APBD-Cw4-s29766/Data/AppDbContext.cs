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
        
        modelBuilder.Entity<ComponentManufacturer>().HasData(
    new ComponentManufacturer { Id = 1, Abbreviation = "AMD", FullName = "Advanced Micro Devices", FoundationDate = new DateTime(1969, 5, 1) },
    new ComponentManufacturer { Id = 2, Abbreviation = "NV", FullName = "NVIDIA Corporation", FoundationDate = new DateTime(1993, 4, 5) },
    new ComponentManufacturer { Id = 3, Abbreviation = "COR", FullName = "Corsair Gaming Inc.", FoundationDate = new DateTime(1994, 1, 1) }
);

modelBuilder.Entity<ComponentType>().HasData(
    new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Processor" },
    new ComponentType { Id = 2, Abbreviation = "GPU", Name = "Graphics Card" },
    new ComponentType { Id = 3, Abbreviation = "RAM", Name = "Memory" }
);

modelBuilder.Entity<Component>().HasData(
    new Component { Code = "CPU0000001", Name = "Ryzen 7 7800X3D", Description = "8-core gaming processor", ComponentManufacturerId = 1, ComponentTypeId = 1 },
    new Component { Code = "GPU0000001", Name = "RTX 4080 Super", Description = "High-end gaming graphics card", ComponentManufacturerId = 2, ComponentTypeId = 2 },
    new Component { Code = "RAM0000001", Name = "Corsair Vengeance DDR5 16GB", Description = "DDR5 RAM module 16GB", ComponentManufacturerId = 3, ComponentTypeId = 3 }
);

modelBuilder.Entity<PC>().HasData(
    new PC { Id = 1, Name = "Gaming Beast X", Weight = 12.5, Warranty = 36, CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0), Stock = 5 },
    new PC { Id = 2, Name = "Office Mini Pro", Weight = 4.2, Warranty = 24, CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0), Stock = 12 },
    new PC { Id = 3, Name = "Creator Station", Weight = 9.8, Warranty = 36, CreatedAt = new DateTime(2026, 3, 20, 10, 15, 0), Stock = 3 }
);

modelBuilder.Entity<PCComponent>().HasData(
    new PCComponent { PCId = 1, ComponentCode = "CPU0000001", Amount = 1 },
    new PCComponent { PCId = 1, ComponentCode = "GPU0000001", Amount = 1 },
    new PCComponent { PCId = 1, ComponentCode = "RAM0000001", Amount = 2 }
);
            
    }
    
}