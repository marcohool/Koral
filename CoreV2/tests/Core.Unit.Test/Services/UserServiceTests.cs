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

        this.jwtOptionsMock.SetupGet(j => j.CurrentValue)
            .Returns(
                new JwtOptions
                {
                    SigningKey =
                        "17hVodg+uiaWSF+qRo1xv8sdrKrSRK+uR9KyvUNCOm1XGVjUBP+kPyn9OsKRJauU\r\nkKDJqJT3F/pDt5fGG9Auoa8qVYUSDNr4V/emlVIO9FKmjjALQ/XwYPj0h2ENliYa\r\nS4sK9vullVs99XNtauDQFtQq8Q7XNcmPL4RSrsrKOM0="
                }
            );

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

        this.mapperMock.VerifyAll();
        this.userManagerMock.VerifyAll();
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

        this.mapperMock.VerifyAll();
        this.userManagerMock.VerifyAll();
    }

    [Fact]
    public async Task LoginAsync_ValidUser_ReturnsToken()
    {
        LoginUserDto loginUserDto = new() { Email = "user@email.com", Password = "testpassword" };

        ApplicationUser user = new() { Email = loginUserDto.Email };

        this.userManagerMock.Setup(um => um.FindByEmailAsync(loginUserDto.Email))
            .ReturnsAsync(user);

        this.signInManagerMock.Setup(sm =>
                sm.CheckPasswordSignInAsync(user, loginUserDto.Password, false)
            )
            .Returns(Task.FromResult(SignInResult.Success));

        LoginUserResponseDto result = await this.userService.LoginAsync(loginUserDto);

        result.Token.Should().NotBeNullOrEmpty();

        this.userManagerMock.VerifyAll();
        this.signInManagerMock.VerifyAll();
    }

    [Fact]
    public async Task LoginAsync_CannotFindUser_ThrowsBadRequestException()
    {
        LoginUserDto loginUserDto = new() { Email = "user@email.com", Password = "testpassword" };

        ApplicationUser user = new() { Email = loginUserDto.Email };

        this.userManagerMock.Setup(um => um.FindByEmailAsync(loginUserDto.Email))
            .ReturnsAsync((ApplicationUser?)null);

        await Assert.ThrowsAsync<BadRequestException>(
            async () => await this.userService.LoginAsync(loginUserDto)
        );

        this.userManagerMock.VerifyAll();
    }

    [Fact]
    public async Task LoginAsync_UserLockedOut_ThrowsBadRequestException()
    {
        LoginUserDto loginUserDto = new() { Email = "user@email.com", Password = "testpassword" };

        ApplicationUser user = new() { Email = loginUserDto.Email };

        this.userManagerMock.Setup(um => um.FindByEmailAsync(loginUserDto.Email))
            .ReturnsAsync(user);

        this.signInManagerMock.Setup(sm =>
                sm.CheckPasswordSignInAsync(user, loginUserDto.Password, false)
            )
            .Returns(Task.FromResult(SignInResult.LockedOut));

        await Assert.ThrowsAsync<BadRequestException>(
            async () => await this.userService.LoginAsync(loginUserDto)
        );

        this.userManagerMock.VerifyAll();
        this.signInManagerMock.VerifyAll();
    }

    [Fact]
    public async Task LoginAsync_SigninFailed_ThrowsBadRequestException()
    {
        LoginUserDto loginUserDto = new() { Email = "user@email.com", Password = "testpassword" };

        ApplicationUser user = new() { Email = loginUserDto.Email };

        this.userManagerMock.Setup(um => um.FindByEmailAsync(loginUserDto.Email))
            .ReturnsAsync(user);

        this.signInManagerMock.Setup(sm =>
                sm.CheckPasswordSignInAsync(user, loginUserDto.Password, false)
            )
            .Returns(Task.FromResult(SignInResult.Failed));

        await Assert.ThrowsAsync<BadRequestException>(
            async () => await this.userService.LoginAsync(loginUserDto)
        );

        this.userManagerMock.VerifyAll();
        this.signInManagerMock.VerifyAll();
    }
}
