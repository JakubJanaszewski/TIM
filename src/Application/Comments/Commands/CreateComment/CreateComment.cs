using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Security;
using Blog.Domain.Entities;

namespace Blog.Application.Posts.Commands.CreatePost;

[Authorize]
public record CreateCommentCommand : IRequest
{
    public required int PostId { get; init; }
    public string? Image { get; init; }
    public required string Content { get; init; }
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
}

public class CreateCommentCommandHandler(IApplicationDbContext context, IUser user, IGeocodingService geocodingService) : IRequestHandler<CreateCommentCommand>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUser _user = user;
    private readonly IGeocodingService _geocodingService = geocodingService;

    public async Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        Post? post = _context.Posts.FirstOrDefault(x => x.Id == request.PostId);

        Guard.Against.NotFound(request.PostId, post);

        string? address = null;
        if (request.Latitude != null && request.Longitude != null)
        {
            address = await _geocodingService.GetAddressAsync((double)request.Latitude, (double)request.Longitude);
        }

        Comment comment = new()
        {
            PostId = request.PostId,
            Image = request.Image,
            Content = request.Content,
            Address = address,
            ApplicationUserId = _user.Id!
        };

        _context.Comments.Add(comment);

        await _context.SaveChangesAsync(cancellationToken);
    }
}

