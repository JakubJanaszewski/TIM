using Blog.Domain.Entities;

namespace Blog.Application.Blogs.Queries.GetBlogs;
public class BlogDto
{
    public required string Title { get; init; }
    public required string ImageUrl { get; set; }
    public required string Content { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<BlogPage, BlogDto>();
        }
    }
}
