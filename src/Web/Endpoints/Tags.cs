using Blog.Application.Tags.Commands.CreateTag;
using Blog.Application.Tags.Commands.DeleteTag;
using Blog.Application.Tags.Queries;

namespace Blog.Web.Endpoints;

public class Tags : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetTags)      
            .MapPost(CreateTag)
            .MapDelete(DeleteTag, "{name}");
    }

    public async Task<List<string>> GetTags(ISender sender, [AsParameters] GetTagsQuery query)
    {
        return await sender.Send(query);
    }

    public async Task CreateTag(ISender sender, CreateTagCommand command)
    {
        await sender.Send(command);
    }

    public async Task<IResult> DeleteTag(ISender sender, string name)
    {
        await sender.Send(new DeleteTagCommand(name));
        return Results.NoContent();
    }
}
