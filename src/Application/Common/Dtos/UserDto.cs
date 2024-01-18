using Blog.Application.Common.Interfaces;

namespace Blog.Application.Common.Dtos;
public class UserDto
{
    public required string? UserName { get; init; }
    public required string? Avatar { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<IUser, UserDto>();
        }
    }
}
