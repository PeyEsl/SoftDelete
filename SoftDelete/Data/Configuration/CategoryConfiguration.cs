using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftDelete.Models.Entities;

namespace SoftDelete.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Title).IsRequired(false).HasMaxLength(100);
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
            builder.Property(c => c.RegistrationDate).HasDefaultValueSql("GETDATE()");
            builder.Property(c => c.EditedDate).IsRequired(false);
            builder.Property(c => c.DeletedDate).IsRequired(false);

            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
