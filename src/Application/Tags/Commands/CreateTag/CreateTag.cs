using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Security;
using Blog.Domain.Entities;

namespace Blog.Application.Tags.Commands.CreateTag;

[Authorize]
public record CreateTagCommand : IRequest
{
    public required string Name { get; init; }
}

public class CreateTagCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateTagCommand>
{
    private readonly IApplicationDbContext _context = context;

    public async Task Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        _context.Tags.Add(new Tag()
        {
            Name = request.Name,
        });

        await _context.SaveChangesAsync(cancellationToken);
    }
}
