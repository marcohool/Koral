using System.ComponentModel.DataAnnotations;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.DataAccess.Persistence.Configurations;

public class ClothingItemConfiguration : IEntityTypeConfiguration<ClothingItem>
{
    public void Configure(EntityTypeBuilder<ClothingItem> builder)
    {
        builder.ToTable("ClothingItems");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Name).IsRequired().HasMaxLength(255);

        builder.Property(ci => ci.Description).HasMaxLength(1024);

        builder.Property(ci => ci.Brand).HasMaxLength(255);

        builder.Property(ci => ci.Category).HasMaxLength(255);

        builder.Property(ci => ci.Colour).HasMaxLength(50);

        builder.Property(ci => ci.Price).HasColumnType("decimal(18,2)");

        builder
            .Property(ci => ci.ImagePath)
            .HasMaxLength(200)
            .HasAnnotation("Url", new UrlAttribute());

        builder
            .Property(ci => ci.SourceUrl)
            .HasMaxLength(200)
            .HasAnnotation("Url", new UrlAttribute())
            .IsRequired();
    }
}
