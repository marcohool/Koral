using Core.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.API.IntegrationTests.TestHelpers;

public class ApplicationDBContextHelper
{
    private readonly string roleId1 = Guid.NewGuid().ToString();
    private readonly string roleId2 = Guid.NewGuid().ToString();

    public void InitialiseDbForTests(ApplicationDBContext context)
    {
        InsertWithIdentity(context, this.CreateClothingItems(), "ClothingItems");
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

    private List<ClothingItem> CreateClothingItems()
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
}
