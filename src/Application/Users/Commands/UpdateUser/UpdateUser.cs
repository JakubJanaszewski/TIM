using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Security;
using Blog.Domain.Constants;

namespace Blog.Application.Tags.Commands.DeleteTag;

[Authorize]
public record UpdateUserCommand : IRequest 
{
    public required string Id { get; init; }
    public required string? NewUserName { get; init; }
    public required string? NewAvatar { get; init; }
}

public class UpdateUserCommandHandler(IIdentityService identityService, IUser user) : IRequestHandler<UpdateUserCommand>
{
    private readonly IIdentityService _identityService = identityService;
    private readonly IUser _user = user;

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (_user.Id != request.Id || !await _identityService.IsInRoleAsync(_user.Id, Roles.Administrator))
        {
            throw new ForbiddenAccessException();
        }

        await _identityService.UpdateUserAsync(request.Id, request.NewUserName!, request.NewAvatar!);
    }
}
