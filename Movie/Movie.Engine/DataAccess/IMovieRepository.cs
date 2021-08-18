using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Movie.Engine.Models;
using Movie.Engine.Models.Enums;

namespace Movie.Engine.DataAccess
{
    public interface IMovieRepository
    {
        /// <summary>
        /// Queries movie data for matched paramters
        /// </summary>
        /// <param name="titleLike">Full/Partial movie title</param>
        /// <param name="yearOfRelease">Release Year</param>
        /// <param name="genres">Subset of genres</param>
        /// <param name="ctx"></param>
        /// <returns>Matched list of movies</returns>
        public Task<IEnumerable<RatedMovie>> GetMoviesAsync(
            string titleLike,
            int? yearOfRelease,
            IEnumerable<Genre> genres,
            CancellationToken ctx = default);

        /// <summary>
        /// Get list of movies with highest average scores
        /// </summary>
        /// <param name="userId">If provided will be used to ratings specific to given user</param>
        /// <param name="count">Total number of movies</param>
        /// <param name="ctx"></param>
        /// <returns>Top 5 list of movies</returns>
        public Task<IEnumerable<RatedMovie>> GetTopRatedAsync(
            int? userId,
            int count,
            CancellationToken ctx = default);

        /// <summary>
        /// Update user's movie rating
        /// </summary>
        /// <param name="userId">UserId (foreign key)</param>
        /// <param name="titleId">TitleId (foreign key)</param>
        /// <param name="score">New Rating</param>
        /// <param name="ctx"></param>
        /// <returns>Whether operation succeeded or not</returns>
        public Task<bool> UpsertRatingAsync(
            int userId,
            int titleId,
            int score,
            CancellationToken ctx = default);
    }
}
