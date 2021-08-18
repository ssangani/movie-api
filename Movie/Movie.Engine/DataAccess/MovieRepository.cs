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
    class MovieRepository : IMovieRepository
    {
        private readonly string _connectionString;
        private readonly IMovieModelMapper _movieModelMapper;
        public MovieRepository(
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
                genres = Flatten(genres),
                titleLike = titleLike,
                yearOfRelease = yearOfRelease
            };
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var multi = await conn.QueryMultipleAsync(MovieRepositorySql.FindMovies, param))
                {
                    var movies = await multi.ReadAsync<MovieDto>();
                    var movieRatings = await multi.ReadAsync<RatingDto>();
                    return _movieModelMapper.Map(movies, movieRatings);
                }
            }
        }

        private string Flatten(IEnumerable<Genre> genres)
        {
            var res = string.Join(',', genres.Select(g => (int)g));
            return string.IsNullOrWhiteSpace(res) ? null : res;
        }

        public async Task<IEnumerable<RatedMovie>> GetTopRatedAsync(
            int? userId,
            int count,
            CancellationToken ctx = default)
        {
            var param = new { 
                userId = userId,
                count = count
            };
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var multi = await conn.QueryMultipleAsync(MovieRepositorySql.GetTopRatedMovies, param))
                {
                    var movies = await multi.ReadAsync<MovieDto>();
                    var movieRatings = await multi.ReadAsync<RatingDto>();
                    return _movieModelMapper.Map(movies, movieRatings);
                }
            }
        }

        public async Task<bool> UpsertRatingAsync(
            int userId,
            int titleId,
            int score,
            CancellationToken ctx = default)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var res = await conn.ExecuteAsync(MovieRepositorySql.UpsertRatings, new
                {
                    userId = userId,
                    movieId = titleId,
                    score = score
                });
                return true;
            }
        }
    }
}
