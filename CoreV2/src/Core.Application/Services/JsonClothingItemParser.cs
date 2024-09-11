using System.Text;
using Core.Application.Dtos.ClothingItem;
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

    public async Task<IEnumerable<ClothingItemImport>> Parse(IFormFile file)
    {
        using Stream stream = file.OpenReadStream();
        using StreamReader streamReader = new(stream, Encoding.UTF8);

        IEnumerable<ClothingItemImport>? json = JsonConvert.DeserializeObject<
            IEnumerable<ClothingItemImport>
        >(await streamReader.ReadToEndAsync(), this.settings);

        if (json is null)
        {
            throw new JsonSerializationException("Deserialized json file was null");
        }

        return json;
    }
}
