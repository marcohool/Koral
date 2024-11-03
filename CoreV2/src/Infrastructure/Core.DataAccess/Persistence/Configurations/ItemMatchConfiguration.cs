using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.DataAccess.Persistence.Configurations;

public class ItemMatchConfiguration : IEntityTypeConfiguration<ItemMatch>
{
    public void Configure(EntityTypeBuilder<ItemMatch> builder)
    {
        builder.ToTable("UploadItemMatches");

        builder.HasKey(uci => new { uci.UploadItemId, uci.ClothingItemId });

        builder
            .HasOne(im => im.UploadItem)
            .WithMany(u => u.ItemMatches)
            .HasForeignKey(im => im.UploadItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(uc => uc.ClothingItem)
            .WithMany(c => c.ItemMatches)
            .HasForeignKey(uc => uc.ClothingItemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
