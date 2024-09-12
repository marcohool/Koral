using Core.Application.Dtos.ClothingItem;
using Core.Application.Models.Parsing;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Services.Interfaces;

public interface IClothingItemParser
{
    Task<ParseResult<ClothingItemImport>> Parse(IFormFile file);
}
