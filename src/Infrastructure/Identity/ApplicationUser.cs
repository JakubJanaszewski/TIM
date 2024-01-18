using Blog.Application.Common.Interfaces;
using Blog.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Blog.Infrastructure.Identity;

public class ApplicationUser : IdentityUser, IUser
{
    public List<Post> Posts { get; set; } = null!;
    public string? Avatar { get; set; }
}
