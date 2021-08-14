using System.Collections.Generic;
using System.Threading.Tasks;
using Movie.Engine.Models;

namespace Movie.Engine
{
    public class MovieEngine : IMovieEngine
    {
        public Task<IEnumerable<MovieInfo>> GetAsync(string titleLike, int? yearOfRelease, string[] genres)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<MovieInfo>> GetTopRatedAsync(int? userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> PutAsync(int userId, int titleId, int rating)
        {
            throw new System.NotImplementedException();
        }
    }
}
