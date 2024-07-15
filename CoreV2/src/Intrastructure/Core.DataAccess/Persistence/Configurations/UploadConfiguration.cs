using Core.Domain.Entities;
using Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.DataAccess.Persistence.Configurations;

public class UploadConfiguration : IEntityTypeConfiguration<Upload>
{
    public void Configure(EntityTypeBuilder<Upload> builder)
    {
        builder.ToTable("Uploads");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Title).HasMaxLength(255);

        builder.Property(u => u.Path).HasMaxLength(1024);

        builder.Property(u => u.Size).IsRequired();

        builder.Property(u => u.ContentType).IsRequired().HasMaxLength(255);

        builder.Property(u => u.Status).HasDefaultValue(UploadStatus.Processing).IsRequired();

        builder.Property(u => u.IsFavourited).IsRequired();

        builder.Property(u => u.IsPinned).IsRequired();

        builder.Property(u => u.IsDeleted).IsRequired();

        builder.Property(u => u.AppUserId).IsRequired().HasMaxLength(50);

        builder
            .HasOne(u => u.AppUser)
            .WithMany(au => au.Uploads)
            .HasForeignKey(a => a.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(u => u.Path).IsUnique();
    }
}
