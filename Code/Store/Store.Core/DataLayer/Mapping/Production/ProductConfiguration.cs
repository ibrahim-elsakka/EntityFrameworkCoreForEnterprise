using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.EntityLayer.Production;

namespace Store.Core.DataLayer.Mapping.Production
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Mapping for table
            builder.ToTable("Product", "Production");

            // Set key for entity
            builder.HasKey(p => p.ProductID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.ProductID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.ProductID).HasColumnType("int").IsRequired();
            builder.Property(p => p.ProductName).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.ProductCategoryID).HasColumnType("int").IsRequired();
            builder.Property(p => p.UnitPrice).HasColumnType("decimal(8, 4)").IsRequired();
            builder.Property(p => p.Description).HasColumnType("varchar(255)");
            builder.Property(p => p.Discontinued).HasColumnType("bit").IsRequired();
            builder.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
            builder.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
            builder.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
            builder.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();

            // Add configuration for foreign keys
            builder
                .HasOne(p => p.ProductCategoryFk)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.ProductCategoryID);

            // Add configuration for uniques
            builder
                .HasAlternateKey(p => new { p.ProductName })
                .HasName("U_ProductName");
        }
    }
}
