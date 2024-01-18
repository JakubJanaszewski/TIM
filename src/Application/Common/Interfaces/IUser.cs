namespace Blog.Application.Common.Interfaces;

public interface IUser
{
    string? Id { get; }

    string? UserName { get; }

    string? Avatar { get; }
}
