namespace Blog.Domain.Entities;
public class Tag : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public List<Post> Posts { get; set; } = null!;
}
