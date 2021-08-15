using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Movie.Engine.DataAccess;
using Movie.Engine.Mappers;
using Movie.Engine.Models;
using Movie.Engine.Models.Enums;

namespace Movie.Engine
{
    public class MovieEngine : IMovieEngine
    {
        private readonly IMovieDao _dao;
        private readonly IMovieInfoMapper _mapper;

        public MovieEngine(
            IMovieDao dao,
            IMovieInfoMapper mapper)
        {
            _dao = dao;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieInfo>> GetAsync(
            string titleLike,
            int? yearOfRelease,
            string[] genres,
            CancellationToken ctx = default)
        {
            if (string.IsNullOrWhiteSpace(titleLike) && !yearOfRelease.HasValue && genres.Length == 0)
                throw new ArgumentException("Query needs to include at least one search term");

            var matchedMovies = await _dao.GetMoviesAsync(titleLike, yearOfRelease, Parse(genres), ctx);
            return matchedMovies.Select(_mapper.Map);
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
