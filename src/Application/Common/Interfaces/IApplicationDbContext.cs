using Blog.Domain.Entities;

namespace Blog.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<BlogPage> Blogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
