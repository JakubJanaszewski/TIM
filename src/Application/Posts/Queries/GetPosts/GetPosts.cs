using System.Collections.Generic;
using Blog.Application.Common.Dtos;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Mappings;
using Blog.Application.Common.Models;
using Blog.Domain.Entities;

namespace Blog.Application.Blogs.Queries.GetPosts;

public record GetPostsWithPaginationQuery : IRequest<PaginatedList<PostDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetPostsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetPostsWithPaginationQuery, PaginatedList<PostDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<PaginatedList<PostDto>> Handle(GetPostsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Posts
            .Include(x => x.Tags)
            .OrderBy(x => x.Created)
            .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
