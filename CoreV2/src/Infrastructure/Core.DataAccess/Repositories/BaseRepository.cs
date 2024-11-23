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

    public IQueryable<TEntity> GetAll(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<TEntity> query = this.dbSet;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return query;
    }

    public async Task AddAsync(TEntity entity)
    {
        this.dbSet.Add(entity);
        await this.context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        this.dbSet.Update(entity);
        await this.context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        this.dbSet.Remove(entity);
        await this.context.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await this.context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        try
        {
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await transaction.DisposeAsync();
        }
    }
}
