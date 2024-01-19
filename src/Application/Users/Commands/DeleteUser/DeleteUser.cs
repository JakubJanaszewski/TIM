using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Security;
using Blog.Domain.Constants;

namespace Blog.Application.Tags.Commands.DeleteTag;

[Authorize(Policy = Policies.CanManageUsers)]
public record DeleteUserCommand(string Id) : IRequest { }

public class DeleteUserCommandHandler(IIdentityService identityService) : IRequestHandler<DeleteUserCommand>
{
    private readonly IIdentityService _identityService = identityService;

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _identityService.DeleteUserAsync(request.Id);
    }
}
