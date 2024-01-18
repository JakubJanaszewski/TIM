using Blog.Application.Common.Dtos;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Mappings;
using Blog.Application.Common.Models;

namespace Blog.Application.Posts.Queries.GetPostsByTags;
public record GetPostsByTagsWithPaginationQuery : IRequest<PaginatedList<PostDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? Tags { get; set; }
}

public class GetPostsByTagsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetPostsByTagsWithPaginationQuery, PaginatedList<PostDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<PaginatedList<PostDto>> Handle(GetPostsByTagsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        if (request.Tags == null || request.Tags.Length == 0)
        {
            return await _context.Posts
                .OrderBy(x => x.Created)
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }

        string[] tags = request.Tags.Split(',');

        return await _context.Posts
                .Where(x => x.Tags.Select(t => t.Name).Any(tg => tags.Any(rt => rt == tg)))
                .OrderBy(x => x.Created)
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}

