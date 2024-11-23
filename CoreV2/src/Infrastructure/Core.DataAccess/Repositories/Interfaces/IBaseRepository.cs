using System.Linq.Expressions;
using Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.DataAccess.Repositories.Interfaces;

public interface IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    IQueryable<TEntity> GetAll(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default
    );

    Task AddAsync(TEntity entity);

    Task UpdateAsync(TEntity entity);

    Task DeleteAsync(TEntity entity);

    Task<IDbContextTransaction> BeginTransactionAsync();

    Task CommitTransactionAsync(IDbContextTransaction transaction);
}
