using Core.Domain.Entities;

namespace Core.DataAccess.Repositories.Interfaces;

public interface IItemMatchRepository
{
    Task AddAsync(ItemMatch uploadMatch);
}
