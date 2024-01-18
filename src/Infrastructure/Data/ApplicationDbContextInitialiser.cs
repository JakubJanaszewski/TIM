using System.Runtime.InteropServices;
using Blog.Domain.Constants;
using Blog.Domain.Entities;
using Blog.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Blog.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };
        var user1 = new ApplicationUser { UserName = "user1", Email = "user1@gmail.com" };
        var user2 = new ApplicationUser { UserName = "user2", Email = "user2@gmail.com" };

        if (!_userManager.Users.Any())
        {
            await _userManager.CreateAsync(administrator, "Admin123!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }

            await _userManager.CreateAsync(user1, "User1Pass!");
            await _userManager.CreateAsync(user2, "User2Pass!");
        }

        Tag sportTag = new()
        {
            Name = "Sport"
        };

        Tag politykaTag = new()
        {
            Name = "Polityka"
        };

        Tag naukaTag = new()
        {
            Name = "Nauka"
        };

        Tag rozrywkaTag = new()
        {
            Name = "Rozrywka"
        };

        //Seed tags
        if (!_context.Tags.Any())
        {
            _context.Tags.AddRange([sportTag, politykaTag, naukaTag, rozrywkaTag]);

            await _context.SaveChangesAsync();
        }

        //Seed posts
        if (!_context.Posts.Any())
        {
            _context.Posts.AddRange([
                new Post()
                {
                    Title = "Lorem Ipsum",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus molestie efficitur dapibus. Nulla facilisi. Integer sodales urna justo, vel congue purus euismod eget. Aenean vel pellentesque massa, ac mollis massa. Ut mattis quam sit amet risus commodo, tincidunt rhoncus tellus lobortis. Donec commodo libero vitae sem sollicitudin rutrum vitae venenatis.",
                    ApplicationUserId = administrator.Id
                },
                new Post()
                {
                    Title = "Neque porro quisquam est qui dolorem ipsum quia",
                    Image = "https://lugers.pl/data/include/cms/Blog-Lugers/AdobeStock_311228231_1.jpeg",
                    Content = "In molestie nulla felis, eu ultrices mi cursus convallis. Praesent dapibus, ligula a interdum elementum, ante dolor commodo eros, sit amet tincidunt mauris sem et sem. Nullam luctus turpis eget commodo commodo. Fusce posuere egestas felis, nec accumsan nibh laoreet non. Maecenas ac rutrum sem. Proin rutrum, est ac scelerisque tempor, tellus nibh sollicitudin purus, quis aliquam eros felis nec metus. Morbi id sollicitudin ante. Proin tristique non lorem ac suscipit. In quis ipsum metus. Pellentesque a cursus sem, in gravida lectus. Phasellus dui dolor, lobortis sed varius sed, maximus vitae enim. Mauris nulla lacus, pretium quis iaculis in, pellentesque nec nunc. Donec semper eros blandit urna suscipit, sed vehicula risus sodales. Aenean posuere rhoncus purus vitae viverra. Quisque at neque in dui tincidunt consequat id sed orci.",
                    Tags = [sportTag],
                    ApplicationUserId = user1.Id
                },
                new Post()
                {
                    Title = "Proin tristique non lorem ac suscipit.",
                    Image = "https://www.exclusive-beds.pl/design/_gallery/_orginal/970.png",
                    Content = "Ut condimentum elit diam, ac laoreet elit faucibus ac. Maecenas sapien velit, consequat a efficitur ac, rutrum quis ante. Nullam eros mauris, vestibulum vel nunc ac, efficitur malesuada elit. Suspendisse faucibus quam eu mauris ullamcorper lacinia. Fusce porta gravida felis sed lobortis. Vestibulum mi mauris, aliquam eget scelerisque ut, molestie porttitor dolor. Integer sed ante quis ex cursus scelerisque. Aliquam vel facilisis neque, quis tempor libero. Praesent mollis condimentum justo nec consectetur. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Nulla ac volutpat nunc, at vestibulum tortor. In a leo et magna molestie vehicula.",
                    Tags = [politykaTag],
                    ApplicationUserId = user1.Id,
                },
                new Post()
                {
                    Title = "Sed id posuere libero.",
                    Content = "Praesent massa augue, faucibus sed porta eget, consectetur nec dui. Vivamus felis eros, scelerisque eget orci a, ullamcorper porta nulla. Nam nulla lacus, euismod eu tellus efficitur, vulputate finibus dui. Phasellus pulvinar mi tortor, vitae iaculis nunc imperdiet quis. Quisque mattis sapien id nisl varius, rutrum porta felis finibus. Nulla facilisi. Donec imperdiet eget ipsum at gravida. Fusce pretium at odio nec malesuada. Nunc dapibus odio ut libero rutrum, sit amet gravida lacus efficitur.",
                    Tags = [naukaTag, rozrywkaTag],
                    ApplicationUserId = user2.Id,
                },
                new Post()
                {
                    Title = "Morbi malesuada molestie mollis.",
                    Image = "https://www.pastelowelove.pl/userdata/public/gfx/5582/kotek-mruczek--naklejka.-naklejka-dla-dzieci.-dekoracje-pokoju.jpg",
                    Content = "Maecenas nec neque efficitur, rutrum velit ac, consectetur libero. Integer a ligula id augue pulvinar pulvinar. Quisque suscipit elit nisl, sollicitudin laoreet neque pulvinar at. Suspendisse potenti. Nulla vulputate venenatis nunc nec sollicitudin. Proin ullamcorper quis dolor et consectetur. Sed faucibus efficitur viverra. In ac orci euismod, tincidunt eros sed, pharetra ligula. Aliquam hendrerit fringilla est, vitae efficitur ligula volutpat euismod. Cras et mauris a nisi condimentum lobortis.",
                    Tags = [sportTag, naukaTag, rozrywkaTag],
                    ApplicationUserId = user2.Id,
                },
                new Post()
                {
                    Title = "Integer eu semper lorem.",
                    Image = "https://www.telekarma.pl/userfiles/images/aktualnosci/305464337-760x500.jpg",
                    Content = "In dolor dui, pellentesque ut posuere vel, tristique vitae est. Sed suscipit a odio vitae viverra. Sed nulla risus, ornare nec placerat vel, porttitor posuere ipsum. Sed dapibus, velit pellentesque scelerisque dictum, eros odio accumsan dolor, non porttitor dolor eros non augue. In eget nisi et arcu iaculis pharetra id nec sapien. In magna arcu, porta vel dolor at, suscipit auctor ante. Maecenas quis arcu rutrum, porta enim ut, ultrices orci. Suspendisse purus mi, tincidunt et dapibus vel, maximus in lectus. Curabitur quis sollicitudin elit. Maecenas at interdum felis. Vestibulum porta volutpat porta. Nunc efficitur suscipit augue porta condimentum. Cras sed eros fringilla libero vestibulum accumsan in sed leo. Suspendisse tempor purus elit, sit amet convallis ligula interdum id. Integer placerat viverra est eget tincidunt.",
                    Tags = [sportTag, politykaTag, naukaTag, rozrywkaTag],
                    ApplicationUserId = user2.Id,
                }
            ]);

            await _context.SaveChangesAsync();
        }
    }
}
