using Blog.Application.Common.Dtos;
using Blog.Application.Common.Interfaces;
using Blog.Domain.Entities;

namespace Blog.Application.Blogs.Queries.GetPosts;

public record GetPostWithCommentsDtoQuery(int PostId) : IRequest<PostWithCommentsDto> {}

public class GetPostWithCommentsDtoQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetPostWithCommentsDtoQuery, PostWithCommentsDto>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<PostWithCommentsDto> Handle(GetPostWithCommentsDtoQuery request, CancellationToken cancellationToken)
    {
        Post? post = await _context.Posts
            .Include(x => x.Comments)
            .FirstOrDefaultAsync(x => x.Id == request.PostId, cancellationToken);

        Guard.Against.NotFound(request.PostId, post);

        post.Comments.Sort((x, y) => DateTimeOffset.Compare(x.Created, y.Created));

        return _mapper.Map<PostWithCommentsDto>(post);
    }
}
