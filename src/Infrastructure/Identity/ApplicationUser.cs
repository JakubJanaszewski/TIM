using Blog.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Blog.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public List<Post> Blogs { get; set; } = null!;
}
