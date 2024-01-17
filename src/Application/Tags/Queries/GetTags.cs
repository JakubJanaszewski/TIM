using Blog.Application.Common.Interfaces;

namespace Blog.Application.Tags.Queries;

public record GetTagsQuery : IRequest<List<string>> {}

public class GetTagsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetTagsQuery, List<string>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<List<string>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Tags
            .OrderBy(x => x.Name)
            .Select(x => x.Name)
            .ToListAsync(cancellationToken);
    }
}

