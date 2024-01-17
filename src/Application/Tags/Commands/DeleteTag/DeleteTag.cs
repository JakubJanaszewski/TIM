using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Security;
using Blog.Domain.Constants;
using Blog.Domain.Entities;

namespace Blog.Application.Tags.Commands.DeleteTag;

[Authorize(Roles = Roles.Administrator)]
public record DeleteTagCommand(string Name) : IRequest { }

public class DeleteTagCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteTagCommand>
{
    private readonly IApplicationDbContext _context = context;

    public async Task Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        Tag? tag = _context.Tags
            .FirstOrDefault(t => t.Name == request.Name);

        Guard.Against.NotFound(request.Name, tag);

        _context.Tags.Remove(tag);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
