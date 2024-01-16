using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Mappings;
using Blog.Application.Common.Models;
using Blog.Domain.Entities;

namespace Blog.Application.Blogs.Queries.GetBlogs;

public record GetTodoItemsWithPaginationQuery : IRequest<PaginatedList<BlogDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetTodoItemsWithPaginationQuery, PaginatedList<BlogDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<PaginatedList<BlogDto>> Handle(GetTodoItemsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Blogs
            .OrderBy(x => x.Title)
            .ProjectTo<BlogDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
