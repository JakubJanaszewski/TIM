using Blog.Application.Posts.Commands.CreatePost;
using Blog.Application.Posts.Commands.DeletePost;

namespace Blog.Web.Endpoints;

public class Comments : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateComment)
            .MapDelete(DeleteComment, "{id}");
    }

    public async Task CreateComment(ISender sender, CreateCommentCommand command)
    {
        await sender.Send(command);
    }

    public async Task<IResult> DeleteComment(ISender sender, int id)
    {
        await sender.Send(new DeleteCommentCommand(id));
        return Results.NoContent();
    }
}
