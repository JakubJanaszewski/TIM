using Blog.Application.Common.Dtos;
using Blog.Application.Common.Interfaces;
using Blog.Application.Tags.Commands.DeleteTag;
using Blog.Application.Tags.Queries;
using Blog.Infrastructure.Identity;

namespace Blog.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetUsers)
            .MapGet(GetCurrentUser, "/Current")
            .MapPut(UpdateUser, "{id}")
            .MapDelete(DeleteUser, "{id}")
            .MapIdentityApi<ApplicationUser>();
            
    }

    public async Task<List<ExtendedUserDto>> GetUsers(ISender sender, [AsParameters] GetUsersQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<ExtendedUserDto> GetCurrentUser(ISender sender, [AsParameters] GetCurrentUserQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<IResult> UpdateUser(ISender sender, string id, UpdateUserCommand command)
    {
        if(command.Id != id)
        {
            Results.BadRequest();
        }
        
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteUser(ISender sender, string id)
    {
        await sender.Send(new DeleteUserCommand(id));
        return Results.NoContent();
    }
}
