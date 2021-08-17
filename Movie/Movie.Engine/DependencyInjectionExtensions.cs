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
            services.AddTransient<IMovieEngine, MovieEngine>();
            services.AddTransient<IMovieInfoMapper, MovieInfoMapper>();
            services.AddSingleton<IMovieDao, MovieDaoStub>();
            return services;
        }
    }
}
