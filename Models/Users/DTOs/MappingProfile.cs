using AutoMapper;
using BringMeBackAPI.Models.Users;
using BringMeBackAPI.Models.Users.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapping User to UserDto
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

        // Mapping RegisterDto to User
        CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => (UserRole)Enum.Parse(typeof(UserRole), src.Role)));
    }
}