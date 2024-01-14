using Microsoft.AspNetCore.Identity;

namespace Blog.Domain.Entities;
public class Blog : BaseAuditableEntity
{
    public string Title { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string Content { get; set; } = null!;

    public Guid UserId { get; set; }
    public IdentityUser User { get; set; } = null!;
}
