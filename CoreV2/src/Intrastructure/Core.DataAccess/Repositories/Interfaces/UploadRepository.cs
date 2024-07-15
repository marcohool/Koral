using Core.DataAccess.Persistence;
using Core.Domain.Entities;

namespace Core.DataAccess.Repositories.Interfaces;

public class UploadRepository(DatabaseContext context)
    : BaseRepository<Upload>(context),
        IUploadRepository;
