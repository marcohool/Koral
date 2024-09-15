using Core.Application.APIs.KoralMatch.Models;
using Microsoft.AspNetCore.Http;

namespace Core.Application.APIs.KoralMatch;

public interface IKoralMatchApi
{
    Task<UploadEmbedding> GetUploadEmbedding(IFormFile upload);
}
