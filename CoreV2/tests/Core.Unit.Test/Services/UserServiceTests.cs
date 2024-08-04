using AutoMapper;
using Core.Application.Configuration;
using Core.Application.Dtos.User;
using Core.Application.Exceptions;
using Core.Application.MappingProfiles;
using Core.Application.Services;
using Core.DataAccess.Identity;
using Core.UnitTest.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.UnitTest.Services;

public class UserServiceTests
{
    private readonly Mock<IMapper> mapperMock;
    private readonly Mock<UserManager<ApplicationUser>> userManagerMock;
    private readonly Mock<SignInManager<ApplicationUser>> signInManagerMock;
    private readonly Mock<IOptionsMonitor<JwtOptions>> jwtOptionsMock;

    private readonly UserService userService;

    public UserServiceTests()
    {
        this.mapperMock = new Mock<IMapper>(MockBehavior.Strict);
        this.userManagerMock = MockHelpers.MockUserManager(new List<ApplicationUser>());
        this.signInManagerMock = MockHelpers.MockSignInManager(this.userManagerMock);
        this.jwtOptionsMock = new Mock<IOptionsMonitor<JwtOptions>>(MockBehavior.Strict);

        this.userService = new UserService(
            this.mapperMock.Object,
            this.userManagerMock.Object,
            this.signInManagerMock.Object,
            this.jwtOptionsMock.Object
        );
    }

    [Fact]
    public async Task CreateAsync_ValidUser_ReturnsCreatedUser()
    {
        CreateUserDto createUserDto =
            new() { Email = "test.email@emails.com", Password = "testpassword" };

        ApplicationUser user = new() { Email = createUserDto.Email };

        this.mapperMock.Setup(m => m.Map<ApplicationUser>(createUserDto)).Returns(user);

        this.userManagerMock.Setup(um =>
                um.CreateAsync(
                    It.Is<ApplicationUser>(u =>
                        u.Email != null && u.Email.Equals(createUserDto.Email)
                    ),
                    createUserDto.Password
                )
            )
            .ReturnsAsync(IdentityResult.Success);

        CreateUserResponseDto result = await this.userService.CreateAsync(createUserDto);

        result.Should().BeEquivalentTo(new CreateUserResponseDto { Id = Guid.Parse(user.Id) });
    }

    [Fact]
    public async Task CreateAsync_InvalidUser_DuplicateEmail_ThrowsBadRequestException()
    {
        CreateUserDto createUserDto =
            new() { Email = "duplicate.email@emails.com", Password = "testpassword" };

        ApplicationUser user = new() { Email = createUserDto.Email };

        this.mapperMock.Setup(m => m.Map<ApplicationUser>(createUserDto)).Returns(user);

        this.userManagerMock.Setup(um =>
                um.CreateAsync(
                    It.Is<ApplicationUser>(u =>
                        u.Email != null && u.Email.Equals(createUserDto.Email)
                    ),
                    createUserDto.Password
                )
            )
            .ReturnsAsync(
                IdentityResult.Failed(new IdentityError { Description = "Duplicate email" })
            );

        await Assert.ThrowsAsync<BadRequestException>(
            async () => await this.userService.CreateAsync(createUserDto)
        );
    }
}
