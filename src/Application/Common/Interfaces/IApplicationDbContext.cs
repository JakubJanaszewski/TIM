using Blog.Domain.Entities;

namespace Blog.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Post> Posts { get; }

    DbSet<Tag> Tags { get; }

    DbSet<Coordinate> Coordinates { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
