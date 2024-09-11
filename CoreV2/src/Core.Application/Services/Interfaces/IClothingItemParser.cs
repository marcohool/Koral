using Core.Application.Dtos.ClothingItem;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Services.Interfaces;

public interface IClothingItemParser
{
    Task<IEnumerable<ClothingItemImport>> Parse(IFormFile file);
}
