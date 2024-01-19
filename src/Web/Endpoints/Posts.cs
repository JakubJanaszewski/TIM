using Blog.Application.Blogs.Queries.GetPosts;
using Blog.Application.Common.Dtos;
using Blog.Application.Common.Models;
using Blog.Application.Posts.Commands.CreatePost;
using Blog.Application.Posts.Commands.DeletePost;
using Blog.Application.Posts.Queries.GetPostsByTags;

namespace Blog.Web.Endpoints;

public class Posts : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetPostsWithPagination)
            .MapGet(GetPostsByTagsWithPagination, "/ByTags")
            .MapGet(GetPostWithComments, "{id}")
            .MapPost(CreatePost)
            .MapDelete(DeletePost, "{id}");
    }

    public async Task<PaginatedList<PostDto>> GetPostsWithPagination(ISender sender, [AsParameters] GetPostsWithPaginationQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<PaginatedList<PostDto>> GetPostsByTagsWithPagination(ISender sender, [AsParameters] GetPostsByTagsWithPaginationQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<PostWithCommentsDto> GetPostWithComments(ISender sender, int id)
    {
        return await sender.Send(new GetPostWithCommentsDtoQuery(id));
    }

    public async Task CreatePost(ISender sender, CreatePostCommand command)
    {
        await sender.Send(command);
    }

    public async Task<IResult> DeletePost(ISender sender, int id)
    {
        await sender.Send(new DeletePostCommand(id));
        return Results.NoContent();
    }
}
