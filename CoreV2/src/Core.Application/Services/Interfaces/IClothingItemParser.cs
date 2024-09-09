using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Services.Interfaces;

public interface IClothingItemParser
{
    Task<List<ClothingItem>> Parse(IFormFile file);
}
