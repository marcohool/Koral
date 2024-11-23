using Microsoft.AspNetCore.Http;

namespace Core.Application.Models.Upload;

public class CreateUploadDto
{
    public required IFormFile Image { get; set; }
}
