using System.Linq.Expressions;
using Core.DataAccess.Persistence;
using Core.DataAccess.Repositories.Interfaces;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.Repositories;

public class UploadRepository(DatabaseContext context)
    : BaseRepository<Upload>(context),
        IUploadRepository
{
    private readonly DbSet<Upload> dbSet = context.Set<Upload>();

    public new async Task<List<Upload>> GetAllAsync(
        Expression<Func<Upload, bool>>? predicate = null,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<Upload> query = this.dbSet;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query
            .Include(ui => ui.UploadItems)
            .ThenInclude(um => um.ItemMatches)
            .ThenInclude(ci => ci.ClothingItem)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
