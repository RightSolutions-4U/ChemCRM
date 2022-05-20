using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserClaimDto, UserClaim>().ReverseMap();
            CreateMap<UserRoleDto, UserRole>().ReverseMap();
            CreateMap<UserAllowedIPDto, UserAllowedIP>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<AddUserCommand, User>();
            CreateMap<ResetPasswordCommand, UserDto>();
        }
    }
}
