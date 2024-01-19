using Blog.Application.Common.Dtos;
using Blog.Application.Common.Interfaces;
using Blog.Domain.Entities;

namespace Blog.Application.Common.Dtos;
public class CommentDto
{
    public required string? Image { get; set; }
    public required string Content { get; set; } = null!;
    public required string? Address { get; set; }
    public required UserDto User { get; set; } = null!;
}

public class CommentDtoMapping : Profile
{
    public CommentDtoMapping() { }

    public CommentDtoMapping(IIdentityService identityService)
    {
        CreateMap<Comment, CommentDto>()
            .ForMember(x => x.User, opt => opt.MapFrom(y => identityService.GetUser(y.ApplicationUserId)));
    }
}
