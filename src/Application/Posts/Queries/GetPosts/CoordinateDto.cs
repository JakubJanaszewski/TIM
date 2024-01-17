using Blog.Application.Blogs.Queries.GetBlogs;
using Blog.Domain.Entities;

namespace Blog.Application.Posts.Queries.GetPosts;
public class CoordinateDto
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Coordinate, CoordinateDto>();
        }
    }
}
