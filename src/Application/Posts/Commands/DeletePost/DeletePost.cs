using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Security;
using Blog.Domain.Constants;
using Blog.Domain.Entities;

namespace Blog.Application.Posts.Commands.DeletePost;

[Authorize]
public record DeletePostCommand(int Id) : IRequest { }

public class DeleteTagCommandHandler(IApplicationDbContext context, IUser user, IIdentityService identityService) : IRequestHandler<DeletePostCommand>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUser _user = user;
    private readonly IIdentityService _identityService = identityService;

    public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        Post? post = _context.Posts
            .FirstOrDefault(x => x.Id == request.Id);

        Guard.Against.NotFound(request.Id, post);

        if(_user.Id != post.ApplicationUserId || !await _identityService.IsInRoleAsync(_user.Id, Roles.Administrator))
        {
            throw new ForbiddenAccessException();
        }

        _context.Posts.Remove(post);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
