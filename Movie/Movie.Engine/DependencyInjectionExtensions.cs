using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Movie.Engine
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMovieEngineServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMovieEngine, MovieEngine>();
            return services;
        }
    }
}
