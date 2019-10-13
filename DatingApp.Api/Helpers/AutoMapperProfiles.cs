using System.Linq;
using AutoMapper;
using DatingApp.Api.Dtos;
using DatingApp.Api.Models;

namespace DatingApp.Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
            .ForMember(d => d.PhotoUrl, o => o.MapFrom(src =>
            src.Photos.FirstOrDefault(p => p.IsMain).Url))
            .ForMember(d => d.Age, opt =>
            opt.MapFrom(src => src.DateofBirth.CalculateAge()));
            CreateMap<User, UserForDetailedDto>().ForMember(d => d.PhotoUrl, o => o.MapFrom(src =>
            src.Photos.FirstOrDefault(p => p.IsMain).Url))
            .ForMember(d => d.Age, opt =>
            opt.MapFrom(src => src.DateofBirth.CalculateAge()));
            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();   
        }
    }
}