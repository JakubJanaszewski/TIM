using System.Reflection;
using Blog.Application.Common.Behaviours;
using Blog.Application.Common.Dtos;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddHttpClient();
        services.AddTransient<IGeocodingService, GeocodingService>();
        services.AddSingleton(provider => new MapperConfiguration(cfg =>
        {
            var identityService = provider.CreateScope().ServiceProvider.GetService<IIdentityService>()!;

            cfg.AddProfile(new PostDtoMapping(identityService));
            cfg.AddProfile(new TagDtoMapping());
            cfg.AddProfile(new UserDtoMapping());
            cfg.AddProfile(new ExtendedUserDtoMapping());
            cfg.AddProfile(new PostWithCommentsDtoMapping(identityService));
            cfg.AddProfile(new CommentDtoMapping(identityService));
        }).CreateMapper());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        });

        return services;
    }
}
