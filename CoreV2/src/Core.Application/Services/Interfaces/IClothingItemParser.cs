using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Services.Interfaces;

public interface IClothingItemParser
{
    Task<IEnumerable<ClothingItem>> Parse(IFormFile file);
}
