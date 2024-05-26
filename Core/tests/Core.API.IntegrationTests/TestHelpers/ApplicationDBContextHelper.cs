using Core.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.API.IntegrationTests.TestHelpers;

public class ApplicationDBContextHelper
{
    public void InitialiseDbForTests(ApplicationDBContext context)
    {
        InsertWithIdentity(context, CreateClothingItems(), "ClothingItems");
        Insert(context, CreateAppUsers());
        Insert(context, CreateRoles());
        Insert(context, CreateImageUploads(context));
    }

    private static void InsertWithIdentity<T>(
        ApplicationDBContext context,
        List<T> entities,
        string tableName
    )
        where T : class
    {
        string onCmd = $"SET IDENTITY_INSERT dbo.{tableName} ON;";
        string offCmd = $"SET IDENTITY_INSERT dbo.{tableName} OFF;";

        context.AddRange(entities);

        using IDbContextTransaction transaction = context.Database.BeginTransaction();
        try
        {
            context.Database.ExecuteSqlRaw(onCmd);
            context.SaveChanges();
            context.Database.ExecuteSqlRaw(offCmd);

            transaction.Commit();
        }
        catch (Exception ex)
        {
            throw new Exception("Error initializing the database: ", ex);
        }
        finally
        {
            context.ChangeTracker.Clear();
        }
    }

    private static void Insert<T>(ApplicationDBContext context, List<T> entities)
        where T : class
    {
        context.AddRange(entities);
        context.SaveChanges();
    }

    private static List<ClothingItem> CreateClothingItems()
    {
        return
        [
            new ClothingItem
            {
                ClothingItemId = 1,
                Name = "T-Shirt",
                Description = "A short-sleeved top",
                Category = "T-Shirts",
                Colour = "White",
                Price = 9.99m,
                SourceURL = "https://www.example.com/t-shirt",
                LastChecked = DateTime.Now,
            },
            new ClothingItem
            {
                ClothingItemId = 2,
                Name = "Jeans",
                Description = "A pair of denim trousers",
                Category = "Jeans",
                Colour = "Blue",
                Price = 19.99m,
                SourceURL = "https://www.example.com/jeans",
                LastChecked = DateTime.Now,
            }
        ];
    }

    private static List<AppUser> CreateAppUsers()
    {
        return
        [
            new AppUser
            {
                Id = "1",
                UserName = "test.email@email.com",
                Email = "test.email@email.com"
            },
            new AppUser
            {
                Id = "2",
                UserName = "test2.email@email.com",
                Email = "test2.email@email.com"
            }
        ];
    }

    private static List<IdentityRole> CreateRoles()
    {
        return
        [
            new IdentityRole
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = "2",
                Name = "User",
                NormalizedName = "USER"
            }
        ];
    }

    private static List<ImageUpload> CreateImageUploads(ApplicationDBContext context)
    {
        AppUser user1 = context.Users.First(u => u.Id == "1");
        AppUser user2 = context.Users.First(u => u.Id == "2");

        return
        [
            new ImageUpload
            {
                ImagePath = "/app/wwwroot/uploads/1.jpg",
                ImageName = "1.jpg",
                ImageSize = 1024,
                ContentType = "image/jpeg",
                UploadedAt = DateTime.Now,
                Status = "Uploaded",
                AppUserId = "1",
                AppUser = user1,
            },
            new ImageUpload
            {
                ImagePath = "/app/wwwroot/uploads/2.jpg",
                ImageName = "2.jpg",
                ImageSize = 1024,
                ContentType = "image/jpeg",
                UploadedAt = DateTime.Now,
                Status = "Uploaded",
                AppUserId = "2",
                AppUser = user2,
            },
        ];
    }
}
