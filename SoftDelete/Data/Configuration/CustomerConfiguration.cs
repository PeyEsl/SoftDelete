using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftDelete.Models.Entities;

namespace SoftDelete.Data.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName).IsRequired(false).HasMaxLength(100);
            builder.Property(c => c.LastName).IsRequired(false).HasMaxLength(100);
            builder.Property(c => c.NationalCode).IsRequired(false).HasMaxLength(10);
            builder.Property(c => c.PhoneNumber).IsRequired(false).HasMaxLength(11);
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
            builder.Property(c => c.RegistrationDate).HasDefaultValueSql("GETDATE()");
            builder.Property(c => c.EditedDate).IsRequired(false);
            builder.Property(c => c.DeletedDate).IsRequired(false);

            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
