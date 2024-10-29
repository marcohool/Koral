using System.Linq.Expressions;
using Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.DataAccess.Repositories.Interfaces;

public interface IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default
    );

    Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);

    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);

    Task AddAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task<Guid> DeleteAsync(TEntity entity);

    Task<IDbContextTransaction> BeginTransactionAsync();

    Task CommitTransactionAsync(IDbContextTransaction transaction);
}
