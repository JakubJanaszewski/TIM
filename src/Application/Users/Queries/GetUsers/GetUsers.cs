using Blog.Application.Common.Dtos;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Security;
using Blog.Domain.Constants;

namespace Blog.Application.Tags.Queries;

[Authorize(Policy = Policies.CanManageUsers)]
public record GetUsersQuery : IRequest<List<ExtendedUserDto>> { }

public class GetUsersQueryHandler(IIdentityService identityService, IMapper mapper) : IRequestHandler<GetUsersQuery, List<ExtendedUserDto>>
{
    private readonly IIdentityService _identityService = identityService;
    private readonly IMapper _mapper = mapper;

    public async Task<List<ExtendedUserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<List<ExtendedUserDto>>(await _identityService.GetUsersAsync());
    }
}
