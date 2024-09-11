using System.Text;
using Core.Application.Services.Interfaces;
using Core.Domain.Entities;
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

    public async Task<IEnumerable<ClothingItem>> Parse(IFormFile file)
    {
        using Stream stream = file.OpenReadStream();
        using StreamReader streamReader = new(stream, Encoding.UTF8);

        IEnumerable<ClothingItem>? json = JsonConvert.DeserializeObject<IEnumerable<ClothingItem>>(
            await streamReader.ReadToEndAsync(),
            this.settings
        );

        if (json is null)
        {
            throw new JsonException("Json file was not deserialized");
        }

        return json;
    }
}
