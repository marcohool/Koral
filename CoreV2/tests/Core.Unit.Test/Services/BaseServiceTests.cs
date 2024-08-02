using Microsoft.AspNetCore.Http;

namespace Core.UnitTest.Services;

public class BaseServiceTests
{
    protected static FormFile CreateMockFormFile(int fileSize, string fileName, string contentType)
    {
        fileSize *= 1024 * 1024;

        byte[] fileContext = new byte[fileSize];
        MemoryStream memoryStream = new(fileContext);

        return new FormFile(memoryStream, 0, fileSize, "file", fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = contentType
        };
    }
}
