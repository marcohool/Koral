using System.Net.Http.Json;
using Core.Application.APIs.KoralMatch.Models;
using Core.Application.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Core.Application.APIs.KoralMatch;

public class KoralMatchApi(
    HttpClient httpClient,
    IOptionsMonitor<KoralMatchConfiguration> koralMatchConfiguration
) : IKoralMatchApi
{
    private readonly HttpClient httpClient = httpClient;
    private readonly IOptionsMonitor<KoralMatchConfiguration> koralMatchConfiguration =
        koralMatchConfiguration;

    public async Task<UploadEmbedding> GetUploadEmbedding(IFormFile upload)
    {
        string uri =
            $"{this.koralMatchConfiguration.CurrentValue.BaseUri}/GenerateEmbedding/upload";

        // TODO: Do not trust certificates in production
        HttpClientHandler handler =
            new()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
        HttpClient httpClient = new(handler);

        HttpResponseMessage uploadEmbeddingResponse = await httpClient.PostAsync(
            uri,
            new MultipartFormDataContent
            {
                { new StreamContent(upload.OpenReadStream()), "image", upload.FileName }
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
