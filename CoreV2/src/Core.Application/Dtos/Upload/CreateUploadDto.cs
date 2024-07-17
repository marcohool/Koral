using Core.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Dtos.Upload;

public class CreateUploadDto
{
    public IFormFile File { get; set; }
}
