using System.Text;
using Core.Application.Models.ClothingItem;
using Core.Application.Models.Parsing;
using Core.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.Application.Services;

public class JsonClothingItemParser : IClothingItemParser
{
    private readonly JsonSerializerSettings settings =
        new()
        {
            MissingMemberHandling = MissingMemberHandling.Error,
            Error = (sender, args) =>
            {
                args.ErrorContext.Handled = false;
            }
        };

    public async Task<ParseResult<ClothingItemImport>> Parse(IFormFile file)
    {
        ParseResult<ClothingItemImport> result = new();

        if (file.Length == 0)
        {
            result.ErrorMessage = "The file is empty.";
            return result;
        }

        await using Stream stream = file.OpenReadStream();
        using StreamReader streamReader = new(stream, Encoding.UTF8);

        try
        {
            string content = await streamReader.ReadToEndAsync();

            IEnumerable<ClothingItemImport>? items = JsonConvert.DeserializeObject<
                IEnumerable<ClothingItemImport>
            >(content, this.settings);

            if (items == null)
            {
                result.ErrorMessage = "Deserialization returned null";
                return result;
            }

            result.Successes = items;

            return result;
        }
        catch (JsonSerializationException ex)
        {
            result.ErrorMessage = $"JSON deserialization failed: {ex.Message}";
            return result;
        }
    }
}
