using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.ModelConfiguration
{
    public class InventoryEventConfiguration : IEntityTypeConfiguration<InventoryEvent>
    {
        public void Configure(EntityTypeBuilder<InventoryEvent> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.ProductId).IsRequired();
            builder.Property(l => l.EventType).IsRequired();
            builder.Property(l => l.ProductName).IsRequired();
            builder.Property(l => l.Timestamp).IsRequired();
        }
    }
}
