using Core.DataAccess.Identity;
using Core.DataAccess.Persistence;
using Core.Domain.Entities;
using Core.Domain.Enums;

namespace Core.Integration.Test.Helpers;

public class DatabaseContextHelper
{
    private static readonly Guid authenticatedUserId = Guid.NewGuid();
    private static readonly Guid user2Id = Guid.NewGuid();

    public static readonly ApplicationUser authenticatedUser =
        new() { Id = authenticatedUserId.ToString(), Email = "testuser@testemail.com" };

    public static readonly ApplicationUser user2 =
        new() { Id = user2Id.ToString(), Email = "user2@testemail.com" };

    private static readonly List<ApplicationUser> Users = [authenticatedUser, user2];

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
            AppUserId = authenticatedUser.Id,
            CreatedOn = DateTime.UtcNow,
            ClothingItems = []
        },
        new Upload
        {
            Id = Guid.NewGuid(),
            Title = "Vintage Autumn Vibe",
            Size = 1024,
            ContentType = "image/jpeg",
            Status = UploadStatus.Processing,
            IsFavourited = false,
            IsPinned = true,
            AccuracyRating = 7,
            IsDeleted = false,
            ImageUrl = "https://example-image-hosting.com/vintage-autumn-vibe.jpg",
            AppUserId = authenticatedUser.Id,
            CreatedOn = DateTime.UtcNow.AddDays(-1),
            ClothingItems = []
        },
        new Upload
        {
            Id = Guid.NewGuid(),
            Title = "Classic Spring Ensemble",
            Size = 1024,
            ContentType = "image/png",
            Status = UploadStatus.Failed,
            IsFavourited = false,
            IsPinned = false,
            AccuracyRating = 9,
            IsDeleted = true,
            ImageUrl = "https://example-image-hosting.com/classic-spring-ensemble.png",
            AppUserId = authenticatedUser.Id,
            CreatedOn = DateTime.UtcNow.AddDays(-2),
            ClothingItems = []
        },
        new Upload
        {
            Id = Guid.NewGuid(),
            Title = "Winter Elegance Series",
            Size = 1024,
            ContentType = "image/jpeg",
            Status = UploadStatus.Processed,
            IsFavourited = true,
            IsPinned = true,
            AccuracyRating = 5,
            IsDeleted = false,
            ImageUrl = "https://example-image-hosting.com/winter-elegance-series.jpg",
            AppUserId = authenticatedUser.Id,
            CreatedOn = DateTime.UtcNow.AddDays(-3),
            ClothingItems = []
        },
        new Upload
        {
            Id = Guid.NewGuid(),
            Title = "Beach Day Outfits",
            Size = 1024,
            ContentType = "image/jpeg",
            Status = UploadStatus.Processed,
            IsFavourited = true,
            IsPinned = false,
            AccuracyRating = 8,
            IsDeleted = false,
            ImageUrl = "https://example-image-hosting.com/beach-day-outfits.jpg",
            AppUserId = user2.Id,
            CreatedOn = DateTime.UtcNow.AddDays(-4),
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
