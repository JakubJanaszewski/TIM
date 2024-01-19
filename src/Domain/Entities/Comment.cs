namespace Blog.Domain.Entities;
public class Comment : BaseAuditableEntity
{
    public string? Image { get; set; }
    public string Content { get; set; } = null!;
    public string? Address { get; set; }
    public string ApplicationUserId { get; set; } = null!;
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
}
