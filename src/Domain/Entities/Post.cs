namespace Blog.Domain.Entities;

public class Post : BaseAuditableEntity
{
    public string Title { get; set; } = null!;
    public string? Image { get; set; }
    public string Content { get; set; } = null!;
    public string? Address { get; set; }
    public List<Tag> Tags { get; set; } = null!;
    public string ApplicationUserId { get; set; } = null!;
}
