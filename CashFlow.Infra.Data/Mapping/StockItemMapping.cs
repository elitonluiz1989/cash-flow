using CashFlow.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashFlow.Infra.Data.Mapping
{
    internal class StockItemMapper : IEntityTypeConfiguration<StockItem>
    {
        public void Configure(EntityTypeBuilder<StockItem> builder)
        {
            builder.ToTable("StockItems");
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.Name)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("VARCHAR(200)");
            builder.Property(prop => prop.Type)
                .IsRequired()
                .HasColumnName("Type")
                .HasColumnType("INTEGER");
            builder.Property(prop => prop.Quantity)
                .IsRequired()
                .HasColumnName("Quantity")
                .HasColumnType("INTEGER");
            builder.Property(prop => prop.Price)
                .IsRequired()
                .HasColumnName("Price")
                .HasColumnType("REAL");
            builder.Property(prop => prop.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasColumnType("TEXT");
            builder.Property(prop => prop.UpdatedAt)
                .HasColumnName("UpdatedAt")
                .HasColumnType("TEXT");
            builder.Property(prop => prop.DeletedAt)
                .HasColumnName("DeletedAt")
                .HasColumnType("TEXT");
        }
    }
}
