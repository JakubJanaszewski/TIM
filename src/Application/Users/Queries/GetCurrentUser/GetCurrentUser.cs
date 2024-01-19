using Blog.Application.Common.Dtos;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Security;

namespace Blog.Application.Tags.Queries;

[Authorize]
public record GetCurrentUserQuery : IRequest<UserDto> { }

public class GetCurrentUserQueryHandler(IUser user, IIdentityService identityService, IMapper mapper) : IRequestHandler<GetCurrentUserQuery, UserDto>
{
    private readonly IUser _user = user;
    private readonly IIdentityService _identityService = identityService;
    private readonly IMapper _mapper = mapper;

    public Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_mapper.Map<UserDto>(_identityService.GetUser(_user.Id!)));
    }
}
