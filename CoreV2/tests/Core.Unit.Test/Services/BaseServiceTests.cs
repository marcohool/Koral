using Core.DataAccess.Identity;

namespace Core.UnitTest.Services;

public class BaseServiceTests
{
    protected readonly ApplicationUser user = new() { Id = Guid.NewGuid().ToString() };
}
