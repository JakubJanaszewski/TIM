using Blog.Domain.Entities;

namespace Blog.Application.Posts.Queries.GetPosts;
public class TagDto
{
    public int Id { get; init; }
    public int Name { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Tag, TagDto>();
        }
    }
}
