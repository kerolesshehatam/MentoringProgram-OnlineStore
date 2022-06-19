using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Catalog.Domain.Entities;

namespace OnlineStore.Catalog.Persistence.EntityConfigurations
{

    public class CategoryItemEntityConfiguration
        : IEntityTypeConfiguration<CategoryItem>
    {
        public void Configure(EntityTypeBuilder<CategoryItem> builder)
        {
            builder.ToTable("CategoryItem");

            builder.Property(ci => ci.Id)
                .IsRequired();

            builder.Property(ci => ci.Name)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(ci => ci.Description)
                .IsRequired(false);

            builder.Property(ci => ci.Image)
                .IsRequired(false);

            builder.Property(ci => ci.Price)
                .IsRequired(true);

            builder.Property(ci => ci.Amount)
                .IsRequired(true);

            builder.HasOne(ci => ci.Category)
           .WithMany()
           .HasForeignKey(ci => ci.CategoryId)
           .IsRequired();

        }
    }
}
