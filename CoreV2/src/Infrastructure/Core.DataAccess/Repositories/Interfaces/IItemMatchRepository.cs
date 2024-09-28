using Core.Domain.Entities;

namespace Core.DataAccess.Repositories.Interfaces;

public interface IItemMatchRepository
{
    Task AddAsync(ItemMatch uploadMatch);

    Task AddRangeAsync(IEnumerable<ItemMatch> uploadMatches);
}
