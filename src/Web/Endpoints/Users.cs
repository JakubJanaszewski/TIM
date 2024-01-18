using Blog.Application.Common.Dtos;
using Blog.Application.Tags.Queries;
using Blog.Infrastructure.Identity;

namespace Blog.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetUsers)
            .MapIdentityApi<ApplicationUser>();
            
    }

    public async Task<List<ExtendedUserDto>> GetUsers(ISender sender, [AsParameters] GetUsersQuery query)
    {
        return await sender.Send(query);
    }
}
