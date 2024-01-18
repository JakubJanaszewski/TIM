using Blog.Application.Common.Interfaces;

namespace Blog.Application.Common.Dtos;
public class ExtendedUserDto
{
    public required string Id { get; init; }
    public required string? UserName { get; init; }
    public required string? Avatar { get; init; }
}

public class ExtendedUserDtoMapping : Profile
{
    public ExtendedUserDtoMapping()
    {
        CreateMap<IUser, ExtendedUserDto>();
    }
}
