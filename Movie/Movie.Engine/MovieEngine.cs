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
        private readonly IMovieRepository _dao;
        private readonly IMovieInfoMapper _mapper;

        public MovieEngine(
            IMovieRepository dao,
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
            // Validate
            if (string.IsNullOrWhiteSpace(titleLike) && !yearOfRelease.HasValue && genres.Length == 0)
                throw new ArgumentException("Query needs to include at least one search term");

            // Return matches
            var matchedMovies = await _dao.GetMoviesAsync(titleLike, yearOfRelease, Parse(genres), ctx);
            return matchedMovies.Select(_mapper.Map);
        }

        public async Task<IEnumerable<MovieInfo>> GetTopRatedAsync(
            int? userId,
            int count,
            CancellationToken ctx = default)
        {
            // TODO: throw an exception if userId or movieId are invalid references
            var matchedMovies = await _dao.GetTopRatedAsync(userId, count, ctx);
            return matchedMovies.Select(_mapper.Map);
        }

        public async Task<bool> PutAsync(
            int userId,
            int titleId,
            int rating,
            CancellationToken ctx = default)
        {
            // Validate
            if (rating > 5 || rating < 1)
                throw new ArgumentException("Rating must be between 1 and 5 (inclusive)");

            // TODO: throw an exception if userId or movieId are invalid references

            // Return Matches
            return await _dao.UpsertRatingAsync(userId, titleId, rating, ctx);
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
