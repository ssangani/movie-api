using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Movie.Engine.Mappers;
using Movie.Engine.Models;
using Movie.Engine.Models.Dto;
using Movie.Engine.Models.Enums;

namespace Movie.Engine.DataAccess
{
    class MovieDao : IMovieDao
    {
        private readonly string _connectionString;
        private readonly IMovieModelMapper _movieModelMapper;
        public MovieDao(
            IMovieModelMapper movieModelMapper)
        {
            _movieModelMapper = movieModelMapper;
            _connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        }

        public async Task<IEnumerable<RatedMovie>> GetMoviesAsync(
            string titleLike,
            int? yearOfRelease,
            IEnumerable<Genre> genres,
            CancellationToken ctx = default)
        {
            var param = new
            {
                genres = string.Join(',', genres.Select(g => (int)g)),
                titleLike = titleLike,
                yearOfRelease = yearOfRelease
            };
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var multi = await conn.QueryMultipleAsync(MovieDaoQuery.GetMoviesSql, param))
                {
                    var movies = (await multi.ReadAsync<MovieDto>()).ToList();
                    var movieRatings = (await multi.ReadAsync<RatingDto>()).ToList();
                    return _movieModelMapper.Map(movies, movieRatings);
                }
            }
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
