using Blog.Domain.Entities;

namespace Blog.Application.Common.Dtos;
public class PostDto
{
    public required string Title { get; init; }
    public required string? Image { get; init; }
    public required string Content { get; init; }
    public required string? Address { get; init; }
    public required List<TagDto> Tags { get; init; } = null!;
    public required DateTimeOffset Created { get; init; }
    public required DateTimeOffset LastModified { get; init; }
    public required UserDto User { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Post, PostDto>();
        }
    }
}
