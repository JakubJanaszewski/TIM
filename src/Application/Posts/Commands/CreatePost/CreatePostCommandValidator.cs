namespace Blog.Application.Posts.Commands.CreatePost;
public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(100);

        RuleFor(x => x.Content)
            .MaximumLength(500);
    }
}
