using api_clean_architecture.Application.UserCQ.Commands;
using api_clean_architecture.Application.UserCQ.ViewModels;
using api_clean_architecture.Domain.Entity;
using AutoMapper;

namespace api_clean_architecture.Application.Mappings
{
    public class ProfileMappings : Profile
    {
        public ProfileMappings()
        {
            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.RefreshToken,x => x.AllowNull())
                .ForMember(dest => dest.PasswordHash, x => x.AllowNull())
                .ForMember(dest => dest.RefreshTokenExpirationTime, map => map.MapFrom(src => AddTenDays()))
                .ForMember(dest => dest.PasswordHash,  map => map.MapFrom(src => src.Password)); 
            
            CreateMap<User, RefreshTokenViewModel>()
                .ForMember(x => x.TokenJwt,  x => x.AllowNull());

            CreateMap<RefreshTokenViewModel, UserInfoViewModel>();
        }
       
        private static DateTime AddTenDays() { return DateTime.Now.AddDays(10); }
    }
}
