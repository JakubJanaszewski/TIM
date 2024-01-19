using Blog.Application.Common.Interfaces;
using Blog.Domain.Entities;

namespace Blog.Application.Common.Dtos;
public class PostWithCommentsDto
{
    public required string? Image { get; init; }
    public required string Content { get; init; }
    public required string? Address { get; init; }
    public required DateTimeOffset Created { get; init; }
    public required DateTimeOffset LastModified { get; init; }
    public required UserDto User { get; init; }
    public required List<CommentDto> Comments { get; init; }
}

public class PostWithCommentsDtoMapping : Profile
{
    public PostWithCommentsDtoMapping() { }

    public PostWithCommentsDtoMapping(IIdentityService identityService)
    {
        CreateMap<Post, PostWithCommentsDto>()
            .ForMember(x => x.User, opt => opt.MapFrom(y => identityService.GetUser(y.ApplicationUserId)));
    }
}
