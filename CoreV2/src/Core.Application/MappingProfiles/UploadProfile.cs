using AutoMapper;
using Core.Application.Dtos.Upload;
using Core.Domain.Entities;

namespace Core.Application.MappingProfiles;

public class UploadProfile : Profile
{
    public UploadProfile()
    {
        this.CreateMap<Upload, UploadDto>();
    }
}
