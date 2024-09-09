using Core.Application.Services.Interfaces;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Services;

public class JsonClothingItemParser : IClothingItemParser
{
    public Task<List<ClothingItem>> Parse(IFormFile file)
    {
        throw new NotImplementedException();
    }
}
