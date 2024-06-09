using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftDelete.Models.Entities;

namespace SoftDelete.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired(false).HasMaxLength(100);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").HasDefaultValue(decimal.Zero);
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.RegistrationDate).HasDefaultValueSql("GETDATE()");
            builder.Property(p => p.EditedDate).IsRequired(false);
            builder.Property(p => p.DeletedDate).IsRequired(false);

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
