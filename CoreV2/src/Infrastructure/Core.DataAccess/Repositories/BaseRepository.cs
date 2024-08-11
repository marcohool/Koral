using System.Linq.Expressions;
using Core.DataAccess.Persistence;
using Core.DataAccess.Repositories.Interfaces;
using Core.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.DataAccess.Repositories;

public class BaseRepository<TEntity>(DatabaseContext context) : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DatabaseContext context = context;
    private readonly DbSet<TEntity> dbSet = context.Set<TEntity>();

    public async Task<List<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        int? pageNumber = null,
        int? pageSize = null
    )
    {
        IQueryable<TEntity> query = this.dbSet;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (pageNumber.HasValue && pageSize.HasValue && pageNumber > 0 && pageSize > 0)
        {
            int skip = (pageNumber.Value - 1) * pageSize.Value;
            query = query.Skip(skip).Take(pageSize.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await this.dbSet.Where(predicate).FirstOrDefaultAsync();
    }

    public async void AddAsync(TEntity entity)
    {
        this.dbSet.Add(entity);
        await this.context.SaveChangesAsync();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        this.dbSet.Update(entity);
        await this.context.SaveChangesAsync();

        return entity;
    }

    public async Task<Guid> DeleteAsync(TEntity entity)
    {
        TEntity removedEntity = this.dbSet.Remove(entity).Entity;
        await this.context.SaveChangesAsync();

        return removedEntity.Id;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await this.context.Database.BeginTransactionAsync();
    }
}
