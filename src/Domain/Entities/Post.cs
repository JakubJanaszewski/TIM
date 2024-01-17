namespace Blog.Domain.Entities;

public class Post : BaseAuditableEntity
{
    public string Title { get; set; } = null!;
    public byte[]? Image { get; set; }
    public string Content { get; set; } = null!;
    public Coordinate? Coordinate { get; set; }
    public List<Tag> Tags { get; set; } = null!;
    public string ApplicationUserId { get; set; } = null!;
}
