using AutoMapper;
using Domain.Common.ValidationAttributes;
using Domain.Entities;
using Domain.ViewModels;

namespace WebApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UploadFile, FileViewModel>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created));
            CreateMap<FileSettings, FileSettingsViewModel>()
                .ForMember(dest => dest.AllowedFileExtensions, opt => opt.MapFrom(src => src.AllowedFileExtensionsList));
        }
    }
}
