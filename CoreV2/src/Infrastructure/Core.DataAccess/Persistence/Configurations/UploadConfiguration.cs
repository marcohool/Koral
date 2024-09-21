using Core.DataAccess.Identity;
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

        builder.Property(u => u.Status).HasDefaultValue(UploadStatus.Pending).IsRequired();

        builder.Property(u => u.IsFavourited).IsRequired();

        builder.Property(u => u.IsPinned).IsRequired();

        builder.Property(u => u.IsDeleted).IsRequired();

        builder
            .HasOne<ApplicationUser>()
            .WithMany(u => u.Uploads)
            .HasForeignKey(u => u.AppUserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
