using Blog.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Blog.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public List<BlogPage> Blogs { get; set; } = null!;
}
