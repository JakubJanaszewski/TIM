using Blog.Application.Common.Models;
using MediatR;

namespace Blog.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);
    Task<Result> DeleteUserAsync(string userId, string newUserName, string newAvatar);
    IUser? GetUser(string userId);
    Task<List<IUser>> GetUsersAsync();

    Task UpdateUserAsync(IUser user);
}
