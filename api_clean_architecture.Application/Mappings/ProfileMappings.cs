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
                .ForMember(dest => dest.RefreshToken,  map => map.MapFrom(src => GenerateGuid()))
                .ForMember(dest => dest.RefreshTokenExpirationTime, map => map.MapFrom(src => AddFiveDays()))
                .ForMember(dest => dest.PasswordHash,  map => map.MapFrom(src => src.Password));

            CreateMap<User, UserInfoViewModel>()
                .ForMember(dest => dest.TokenJwt, map => map.MapFrom(src => GenerateGuid()));
        }

        private static string GenerateGuid() {  return Guid.NewGuid().ToString(); }
        private static DateTime AddFiveDays() { return DateTime.Now.AddDays(5); }
    }
}
