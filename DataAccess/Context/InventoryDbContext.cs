using DataAccess.ModelConfiguration;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

    // Constructor sin parámetros para migraciones
    public InventoryDbContext() { }

    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<InventoryEvent> InventoryEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new InventoryEventConfiguration()); // Aplica configuración
    }

    // Configura SQLite por defecto para migraciones
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=inventory.db");
        }
    }
}
