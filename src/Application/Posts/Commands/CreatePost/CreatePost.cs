using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Security;
using Blog.Domain.Entities;

namespace Blog.Application.Posts.Commands.CreatePost;

[Authorize]
public record CreatePostCommand : IRequest
{
    public required string Title { get; init; }
    public string? Image { get; init; }
    public required string Content { get; init; }
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
    public required List<string> Tags { get; init; }
}

public class CreatePostCommandHandler(IApplicationDbContext context, IUser user, IGeocodingService geocodingService) : IRequestHandler<CreatePostCommand>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUser _user = user;
    private readonly IGeocodingService _geocodingService = geocodingService;

    public async Task Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        string? address = null;
        if(request.Latitude != null && request.Longitude != null)
        {
            address = await _geocodingService.GetAddressAsync((double)request.Latitude, (double)request.Longitude);
        }

        List<Tag> tags = _context.Tags
            .Where(x => request.Tags.Contains(x.Name))
            .ToList();

        Post post = new()
        {
            Title = request.Title,
            Image = request.Image,
            Content = request.Content,
            Address = address,
            Tags = tags,
            ApplicationUserId = _user.Id!
        };

        _context.Posts.Add(post);

        await _context.SaveChangesAsync(cancellationToken);
    }
}

