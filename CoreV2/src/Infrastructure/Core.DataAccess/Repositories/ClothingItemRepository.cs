using Core.DataAccess.Persistence;
using Core.DataAccess.Repositories.Interfaces;
using Core.Domain.Entities;

namespace Core.DataAccess.Repositories;

public class ClothingItemRepository(DatabaseContext context)
    : BaseRepository<ClothingItem>(context),
        IClothingItemRepository;
