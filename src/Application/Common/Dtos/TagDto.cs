using Blog.Domain.Entities;

namespace Blog.Application.Common.Dtos;
public class TagDto
{
    public int Name { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Tag, TagDto>();
        }
    }
}
