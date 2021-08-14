using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movie.Engine.DataAccess;

namespace Movie.Engine
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMovieEngineServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMovieEngine, MovieEngine>();
            services.AddSingleton<IMovieDao, MovieDaoStub>();
            return services;
        }
    }
}
