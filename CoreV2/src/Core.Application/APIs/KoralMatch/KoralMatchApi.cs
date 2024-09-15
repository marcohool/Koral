using System.Net.Http.Json;
using Core.Application.APIs.KoralMatch.Models;
using Microsoft.AspNetCore.Http;

namespace Core.Application.APIs.KoralMatch;

public class KoralMatchApi(HttpClient httpClient) : IKoralMatchApi
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<UploadEmbedding> GetUploadEmbedding(IFormFile upload)
    {
        HttpResponseMessage uploadEmbeddingResponse = await this.httpClient.PostAsync(
            "api/koral-match/upload-embedding",
            new MultipartFormDataContent
            {
                { new StreamContent(upload.OpenReadStream()), "upload", upload.FileName }
            }
        );

        uploadEmbeddingResponse.EnsureSuccessStatusCode();

        UploadEmbedding? uploadEmbedding =
            await uploadEmbeddingResponse.Content.ReadFromJsonAsync<UploadEmbedding>();

        if (uploadEmbedding is null)
        {
            throw new HttpRequestException("Failed to get upload embedding from KoralMatch API.");
        }

        return uploadEmbedding;
    }
}
