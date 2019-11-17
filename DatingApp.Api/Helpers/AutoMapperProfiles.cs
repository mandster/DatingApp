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
            opt.MapFrom(src => src.DateofBirth.CalculateAge()))
             .ForMember(d => d.Likees, o => o.MapFrom(src =>
            src.Likees.Where(p => p.LikerId == src.Id)));
            
            CreateMap<User, UserForDetailedDto>()
            .ForMember(d => d.PhotoUrl, 
            o => o.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
            .ForMember(d => d.Age, opt =>
            opt.MapFrom(src => src.DateofBirth.CalculateAge()))
             .ForMember(d => d.Likees, o => o.MapFrom(src =>
            src.Likees.Where(p => p.LikerId == src.Id)));

            CreateMap<Like, LikeesForReturnDto>();

            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Message, MessagesToReturnDto>()
            .ForMember(m => m.SenderPhotoUrl, opt => opt
            .MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
            .ForMember(m => m.RecipientPhotoUrl, opt => opt
            .MapFrom(u => u.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));


        }
    }
}