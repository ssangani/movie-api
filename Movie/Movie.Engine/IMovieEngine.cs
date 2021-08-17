using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Movie.Engine.Models;

namespace Movie.Engine
{
    public interface IMovieEngine
    {
        /// <summary>
        /// Fetches a list of movies matching given criteria
        /// </summary>
        /// <param name="titleLike">Full/Partial to title name to match against (if provided)</param>
        /// <param name="yearOfRelease">Year of Release to match against (if provided)</param>
        /// <param name="genres">List Of genres to match against (if provided)</param>
        /// <returns>List of matched movies</returns>
        public Task<IEnumerable<MovieInfo>> GetAsync(
            string titleLike,
            int? yearOfRelease,
            string[] genres,
            CancellationToken ctx = default);

        /// <summary>
        /// Fetches a list of top rated movies
        /// </summary>
        /// <param name="userId">Based on particular user's ratings (if provided)</param>
        /// <param name="count">Number of movies</param>
        /// <returns>List of top 5 highly rated movies</returns>
        public Task<IEnumerable<MovieInfo>> GetTopRatedAsync(
            int? userId,
            int count,
            CancellationToken ctx = default);

        /// <summary>
        /// Upserts rating for given user-title pair
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="titleId">TitleId</param>
        /// <param name="rating">Rating</param>
        /// <returns>Whether operation succeeded or not</returns>
        public Task<bool> PutAsync(
            int userId,
            int titleId,
            int rating,
            CancellationToken ctx = default);
    }
}
