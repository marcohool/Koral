using System.Security.Claims;
using Core.API.Configuration;
using Core.API.Dto.ImageUpload;
using Core.API.Models;
using Core.API.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Core.API.Services;

/// <inheritdoc/>
public class ImageUploadService(
    IOptions<ImageUploadSettings> imageUploadSettings,
    IImageUploadRepository imageUploadRepository,
    IHttpContextAccessor httpContextAccessor,
    UserManager<AppUser> userManager,
    IWebHostEnvironment environment
) : IImageUploadService
{
    private readonly IImageUploadRepository imageUploadRepository = imageUploadRepository;
    private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
    private readonly UserManager<AppUser> userManager = userManager;
    private readonly IWebHostEnvironment environment = environment;

    private readonly long maxFileSize = imageUploadSettings.Value.MaxFileSize;
    private readonly string[] allowedFileExtensions = imageUploadSettings
        .Value
        .AllowedFileExtensions;

    /// <inheritdoc/>
    public async Task<ImageUploadResponse> UploadImageAsync(ImageUploadRequest imageUpload)
    {
        IFormFile imageFile = imageUpload.ImageFile;

        (bool isValid, string? errorMessage) = this.ValidateImageFile(imageFile);

        if (!isValid)
            return new ImageUploadResponse { Success = false, ErrorMessage = errorMessage };

        AppUser? user = await this.GetCurrentUserAsync();

        if (user == null)
            return new ImageUploadResponse
            {
                Success = false,
                ErrorMessage = "User not authenticated."
            };

        (string fileName, string filePath, long fileSize) = await this.CreateImage(imageFile);

        try
        {
            ImageUpload upload = await this.imageUploadRepository.CreateImageUpload(
                new ImageUpload
                {
                    AppUserId = user.Id,
                    ImageName = fileName,
                    ImagePath = filePath,
                    ImageSize = fileSize,
                    ContentType = imageFile.ContentType,
                    UploadedAt = DateTime.UtcNow,
                    Status = "Uploaded",
                    AppUser = user
                }
            );

            return new ImageUploadResponse
            {
                Success = true,
                ImageId = upload.ImageUploadId,
                ImagePath = upload.ImagePath
            };
        }
        catch (Exception ex)
        {
            // To-do: Delete the uploaded image file if the database operation

            return new ImageUploadResponse { Success = false, ErrorMessage = ex.Message };
        }
    }

    private (bool isValid, string? errorMessage) ValidateImageFile(IFormFile imageFile)
    {
        if (imageFile is null || imageFile.Length == 0)
            return (false, "No file uploaded.");

        if (imageFile.Length > this.maxFileSize)
        {
            return (false, $"File size should not exceed {this.maxFileSize / (1024 * 1024)} MB.");
        }

        string extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

        if (!this.allowedFileExtensions.Contains(extension))
            return (false, $"Invalid file type {extension}.");

        return (true, null);
    }

    private async Task<(string fileName, string filePath, long fileSize)> CreateImage(
        IFormFile imageFile
    )
    {
        string uploadFolderPath = Path.Combine(this.environment.WebRootPath, "uploads");

        if (!Directory.Exists(uploadFolderPath))
        {
            Directory.CreateDirectory(uploadFolderPath);
        }

        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
        string filePath = Path.Combine(uploadFolderPath, fileName);
        long fileSize = imageFile.Length;

        using (FileStream fileStream = new(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(fileStream);
        }

        return (fileName, filePath, fileSize);
    }

    private async Task<AppUser?> GetCurrentUserAsync()
    {
        IHttpContextAccessor accessor = this.httpContextAccessor;

        HttpContext? content = accessor.HttpContext;

        ClaimsPrincipal? user2 = content?.User;

        ClaimsPrincipal? user = this.httpContextAccessor.HttpContext?.User;
        return user != null ? await this.userManager.GetUserAsync(user) : null;
    }
}
