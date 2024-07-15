using System.Linq.Expressions;
using Core.DataAccess.Persistence;
using Core.DataAccess.Repositories.Interfaces;
using Core.Domain.Common;
using Core.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.Repositories;

public class BaseRepository<TEntity>(DatabaseContext context) : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> dbSet = context.Set<TEntity>();

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
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

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        TEntity removedEntity = this.dbSet.Remove(entity).Entity;
        await context.SaveChangesAsync();

        return removedEntity;
    }
}
