using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movie.Engine.DataAccess;
using Movie.Engine.Mappers;

namespace Movie.Engine
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMovieEngineServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMovieModelMapper, MovieModelMapper>();
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IMovieInfoMapper, MovieInfoMapper>();
            services.AddTransient<IMovieEngine, MovieEngine>();
            return services;
        }
    }
}
