using Blog.Application.Common.Dtos;
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
    public CoordinateDto? Coordinate { get; init; }
    public required List<string> Tags { get; init; }
}

public class CreatePostCommandHandler(IApplicationDbContext context, IUser user) : IRequestHandler<CreatePostCommand>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUser _user = user;

    public async Task Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        Coordinate? coordinate = request.Coordinate == null ? null : new Coordinate() { Latitude = request.Coordinate.Latitude, Longitude = request.Coordinate.Longitude };
        List<Tag> tags = _context.Tags
            .Where(x => request.Tags.Contains(x.Name))
            .ToList();

        Post post = new()
        {
            Title = request.Title,
            Image = request.Image,
            Content = request.Content,
            Coordinate = coordinate,
            Tags = tags,
            ApplicationUserId = _user.Id!
        };

        _context.Posts.Add(post);

        await _context.SaveChangesAsync(cancellationToken);
    }
}

