using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Movie.Engine.DataAccess;
using Movie.Engine.Models;
using Movie.Engine.Models.Enums;

namespace Movie.Engine
{
    public class MovieEngine : IMovieEngine
    {
        private readonly IMovieDao _dao;

        public MovieEngine(
            IMovieDao dao)
        {
            _dao = dao;
        }

        public Task<IEnumerable<MovieInfo>> GetAsync(
            string titleLike,
            int? yearOfRelease,
            string[] genres,
            CancellationToken ctx = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<MovieInfo>> GetTopRatedAsync(
            int? userId,
            CancellationToken ctx = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> PutAsync(
            int userId,
            int titleId,
            int rating,
            CancellationToken ctx = default)
        {
            throw new System.NotImplementedException();
        }

        private IEnumerable<Genre> Parse(string[] rawGenres)
        {
            var result = new HashSet<Genre>();
            foreach (var rawGenre in rawGenres)
            {
                if (Enum.TryParse<Genre>(rawGenre, true, out var genre))
                    result.Add(genre);
            }

            return result;
        }
    }
}
