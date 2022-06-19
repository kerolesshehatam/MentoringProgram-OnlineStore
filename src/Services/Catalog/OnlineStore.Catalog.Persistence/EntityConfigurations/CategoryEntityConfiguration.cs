using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Catalog.Application.Models.Responses;
using OnlineStore.Catalog.Domain.Entities;

namespace OnlineStore.Catalog.Persistence.EntityConfigurations
{

    public class CategoryEntityConfiguration
        : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.Property(ci => ci.Id)
                .IsRequired();

            builder.Property(ci => ci.Name)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(ci => ci.Image)
                .IsRequired(false);

            builder.HasOne(ci => ci.Partent)
                .WithOne()
                .IsRequired(false);

        }
    }
}
