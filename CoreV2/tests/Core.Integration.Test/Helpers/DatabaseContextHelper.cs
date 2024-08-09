using Core.DataAccess.Identity;
using Core.DataAccess.Persistence;
using Core.Domain.Entities;
using Core.Domain.Enums;

namespace Core.Integration.Test.Helpers;

public class DatabaseContextHelper
{
    private static readonly Guid user1Id = Guid.NewGuid();

    private static readonly List<ApplicationUser> Users =
    [
        new ApplicationUser { Id = user1Id.ToString(), Email = "testuser@testemail.com" }
    ];

    private static readonly List<ClothingItem> ClothingItems =
    [
        new ClothingItem
        {
            Id = Guid.NewGuid(),
            Name = "White T-Shirt",
            Description = "A simple and plain white t-shirt",
            Brand = "H&M",
            Category = "T-Shirts",
            Colour = "White",
            Price = 9.99m,
            CurrencyCode = "GBP",
            Gender = Gender.Male,
            ImageUrl = "https://example-image-hosting.com/white-t-shirt.jpg",
            SourceUrl = "https://example.com/white-t-shirt",
            SourceRegion = SourceRegion.UK,
            CreatedOn = DateTime.UtcNow,
            LastUpdatedOn = null,
            Uploads = []
        }
    ];

    private static readonly List<Upload> Uploads =
    [
        new Upload
        {
            Id = Guid.NewGuid(),
            Title = "Old Money Summer Look",
            Size = 1024,
            ContentType = "image/jpeg",
            Status = UploadStatus.Processed,
            IsFavourited = true,
            IsPinned = false,
            AccuracyRating = 8,
            IsDeleted = false,
            ImageUrl = "https://example-image-hosting.com/old-money-summer-look.jpg",
            AppUserId = user1Id.ToString(),
            CreatedOn = DateTime.UtcNow,
            ClothingItems = []
        }
    ];

    public DatabaseContextHelper(DatabaseContext context)
    {
        context.AddRange(Users);
        context.SaveChanges();
        context.ChangeTracker.Clear();

        context.AddRange(ClothingItems);
        context.AddRange(Uploads);

        context.SaveChanges();
    }
}
