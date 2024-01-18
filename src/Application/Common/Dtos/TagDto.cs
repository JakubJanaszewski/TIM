using Blog.Domain.Entities;

namespace Blog.Application.Common.Dtos;
public class TagDto
{
    public required string Name { get; init; }
}

public class TagDtoMapping : Profile
{
    public TagDtoMapping()
    {
        CreateMap<Tag, TagDto>();
    }
}
