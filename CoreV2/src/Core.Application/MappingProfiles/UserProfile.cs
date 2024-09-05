using AutoMapper;
using Core.Application.Dtos.User;
using Core.DataAccess.Identity;

namespace Core.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        this.CreateMap<CreateUserDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => Guid.NewGuid()));
    }
}
