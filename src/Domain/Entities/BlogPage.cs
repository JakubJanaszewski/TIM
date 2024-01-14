using Microsoft.AspNetCore.Identity;

namespace Blog.Domain.Entities;
public class BlogPage : BaseAuditableEntity
{
    public string Title { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string Content { get; set; } = null!;
}
