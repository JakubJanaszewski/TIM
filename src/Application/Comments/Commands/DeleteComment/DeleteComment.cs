using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Security;
using Blog.Domain.Constants;
using Blog.Domain.Entities;

namespace Blog.Application.Posts.Commands.DeletePost;

[Authorize]
public record DeleteCommentCommand(int Id) : IRequest { }

public class DeleteCommentCommandHandler(IApplicationDbContext context, IUser user, IIdentityService identityService) : IRequestHandler<DeleteCommentCommand>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUser _user = user;
    private readonly IIdentityService _identityService = identityService;

    public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        Comment? comment = _context.Comments
            .FirstOrDefault(x => x.Id == request.Id);

        Guard.Against.NotFound(request.Id, comment);

        if (_user.Id != comment.ApplicationUserId || !await _identityService.IsInRoleAsync(_user.Id, Roles.Administrator))
        {
            throw new ForbiddenAccessException();
        }

        _context.Comments.Remove(comment);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
