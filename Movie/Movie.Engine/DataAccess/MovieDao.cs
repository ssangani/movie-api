using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Movie.Engine.Models;
using Movie.Engine.Models.Enums;

namespace Movie.Engine.DataAccess
{
    class MovieDao : IMovieDao
    {
        public MovieDao()
        {

        }

        public Task<IEnumerable<RatedMovie>> GetMoviesAsync(
            string titleLike,
            int? yearOfRelease,
            IEnumerable<Genre> genres,
            CancellationToken ctx = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<RatedMovie>> GetTopRatedAsync(
            int? userId,
            int count,
            CancellationToken ctx = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpsertRatingAsync(
            int userId,
            int titleId,
            int score,
            CancellationToken ctx = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
