using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Movie.Engine.Models;
using Movie.Engine.Models.Enums;

namespace Movie.Engine.DataAccess
{
    public interface IMovieDao
    {
        public Task<IEnumerable<RatedMovie>> GetMoviesAsync(
            string titleLike,
            int? yearOfRelease,
            IEnumerable<Genre> genres,
            CancellationToken ctx = default);
    }
}
