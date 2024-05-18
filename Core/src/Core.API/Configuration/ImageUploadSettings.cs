namespace Core.API.Configuration;

public class ImageUploadSettings
{
    public long MaxFileSize { get; set; }

    public string[] AllowedFileExtensions { get; set; }
}
