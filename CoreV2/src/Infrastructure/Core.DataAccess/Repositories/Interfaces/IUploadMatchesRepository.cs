using Core.Domain.Entities;

namespace Core.DataAccess.Repositories.Interfaces;

public interface IUploadMatchesRepository
{
    Task AddAsync(UploadMatch uploadMatch);
}
