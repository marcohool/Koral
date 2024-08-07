using System.Linq.Expressions;
using Core.DataAccess.Persistence;
using Core.DataAccess.Repositories.Interfaces;
using Core.Domain.Common;
using Core.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.DataAccess.Repositories;

public class BaseRepository<TEntity>(DatabaseContext context) : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> dbSet = context.Set<TEntity>();

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        if (predicate == null)
        {
            return await this.dbSet.ToListAsync();
        }

        return await this.dbSet.Where(predicate).ToListAsync();
    }

    public async Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> predicate)
    {
        TEntity? entity = await this.dbSet.Where(predicate).FirstOrDefaultAsync();

        if (entity == null)
            throw new ResourceNotFoundException(typeof(TEntity));

        return await this.dbSet.Where(predicate).FirstOrDefaultAsync();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        context.ChangeTracker.Clear();
        TEntity addedEntity = (await this.dbSet.AddAsync(entity)).Entity;
        await context.SaveChangesAsync();
        return addedEntity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        this.dbSet.Update(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    public async Task<Guid> DeleteAsync(TEntity entity)
    {
        TEntity removedEntity = this.dbSet.Remove(entity).Entity;
        await context.SaveChangesAsync();

        return removedEntity.Id;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await context.Database.BeginTransactionAsync();
    }
}
