using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Movie.Engine.Models;
using Movie.Engine.Models.Dto;
using Movie.Engine.Models.Enums;

namespace Movie.Engine.DataAccess
{
    /// <summary>
    /// TODO: Replace with a dockerized SQL container loaded with static data set
    /// </summary>
    public class MovieDaoStub : IMovieDao
    {
        private const int MinRating = 1;
        private const int MaxRating = 5;

        private static readonly List<UserDto> _users;
        private static readonly List<MovieModel> _movies;
        private static readonly List<RatingDto> _ratings;
        private static readonly SemaphoreSlim _semaphore;

        static MovieDaoStub()
        {
            _users = SeedUsers().ToList();
            _movies = SeedMovies().ToList();
            _ratings = SeedRatings().ToList();
            _semaphore = new SemaphoreSlim(1, 1);
        }

        public async Task<IEnumerable<RatedMovie>> GetMoviesAsync(
            string titleLike,
            int? yearOfRelease,
            IEnumerable<Genre> genres,
            CancellationToken ctx = default)
        {
            await Task.Yield();
            return GetMovieModels(titleLike, yearOfRelease, genres)
                .Select(movie => new RatedMovie
                {
                    Movie = movie,
                    Ratings = GetRatingsByTitle(movie.Id)
                });
        }

        public async Task<IEnumerable<RatedMovie>> GetTopRatedAsync(
            int? userId,
            int count,
            CancellationToken ctx = default)
        {
            await Task.Yield();
            var titleIds = GetTopRatedTitles(userId)
                .Take(count)
                .ToHashSet();

            return _movies
                .Where(m => titleIds.Contains(m.Id))
                .Select(movie => new RatedMovie
                {
                    Movie = movie,
                    Ratings = GetRatingsByTitle(movie.Id)
                });
        }

        public async Task<bool> UpsertRatingAsync(
            int userId,
            int titleId,
            int score,
            CancellationToken ctx = default)
        {
            await Task.Yield();

            // Invalidate if user or title isn't found
            if (!_users.Any(user => user.Id == userId))
                return false;

            if (!_movies.Any(movie => movie.Id == titleId))
                return false;

            // Upsert the score
            await _semaphore.WaitAsync();
            try
            {
                _ratings.RemoveAll(r => r.UserId == userId && r.MovieId == titleId);
            }
            finally
            {
                var random = new Random();
                _ratings.Add(new RatingDto
                {
                    Id = random.Next(),
                    MovieId = titleId,
                    UserId = userId,
                    Score = score
                });
                _semaphore.Release();
            }

            return true;
        }

        private IEnumerable<int> GetTopRatedTitles(int? userId)
        {
            IEnumerable<RatingDto> ratings = _ratings;
            if (userId.HasValue)
            {
                // Invalidate if user foreign key reference isn't found
                if (!_users.Any(user => user.Id == userId))
                    throw new ArgumentException("User not found");

                ratings = ratings.Where(r => r.UserId == userId.Value);
            }

            return ratings
                .GroupBy(r => r.MovieId, r => r)
                .Select(titleGroup => (
                    AvgScore: titleGroup.Select(titleGroup => titleGroup.Score).Average(),
                    TitleId: titleGroup.Key))
                .OrderByDescending(x => x.AvgScore)
                .OrderBy(x => GetMovieTitle(x.TitleId))
                .Select(x => x.TitleId);
        }

        private IEnumerable<MovieModel> GetMovieModels (string titleLike, int? yearOfRelease, IEnumerable<Genre> genres)
        {
            IEnumerable<MovieModel> movies = _movies;

            if (!string.IsNullOrWhiteSpace(titleLike))
            {
                movies = movies.Where(m => m.Title.Contains(titleLike, StringComparison.InvariantCultureIgnoreCase));
            }

            if (yearOfRelease.HasValue)
            {
                movies = movies.Where(m => m.ReleaseYear == yearOfRelease.Value);
            }

            if (genres.Count() > 0)
            {
                movies = movies.Where(m => genres.All(genre => m.Genres.Contains(genre)));
            }

            return movies;
        }

        private string GetMovieTitle(int id)
        {
            return _movies.Where(m => m.Id == id).FirstOrDefault().Title;
        }

        private IEnumerable<RatingDto> GetRatingsByTitle(int titleId)
        {
            return _ratings.Where(r => r.MovieId == titleId);
        }

        private static IEnumerable<UserDto> SeedUsers()
        {
            yield return new UserDto { Id = 1, Username = "farley" };
            yield return new UserDto { Id = 2, Username = "jakob" };
            yield return new UserDto { Id = 3, Username = "patootie" };
            yield return new UserDto { Id = 4, Username = "firebug" };
            yield return new UserDto { Id = 5, Username = "foxyred" };
            yield return new UserDto { Id = 6, Username = "dionelso" };
            yield return new UserDto { Id = 7, Username = "ultalmar" };
            yield return new UserDto { Id = 8, Username = "acelthes" };
            yield return new UserDto { Id = 9, Username = "saursimo" };
            yield return new UserDto { Id = 10, Username = "mariumse" };
            yield return new UserDto { Id = 11, Username = "nushrono" };
            yield return new UserDto { Id = 12, Username = "sanguine" };
        }

        private static IEnumerable<MovieModel> SeedMovies()
        {
            int counter = 1;
            yield return new MovieModel {
                Id = counter++,
                Title = "My Man Godfrey",
                ReleaseYear = 1936,
                Genres = new[] { Genre.Comedy, Genre.Drama, Genre.Romance },
                RunningTime = new TimeSpan(1, 34, 0) 
            };
            yield return new MovieModel
            {
                Id = counter++,
                Title = "It Happened One Night",
                ReleaseYear = 1934,
                Genres = new[] { Genre.Comedy, Genre.Romance },
                RunningTime = new TimeSpan(1, 45, 0)
            };
            yield return new MovieModel
            {
                Id = counter++,
                Title = "The Apartment",
                ReleaseYear = 1960,
                Genres = new[] { Genre.Comedy, Genre.Drama, Genre.Romance },
                RunningTime = new TimeSpan(2, 5, 0)
            };
            yield return new MovieModel
            {
                Id = counter++,
                Title = "How to Steal a Million",
                ReleaseYear = 1966,
                Genres = new[] { Genre.Comedy, Genre.Crime, Genre.Romance },
                RunningTime = new TimeSpan(2, 3, 0)
            };
            yield return new MovieModel
            {
                Id = counter++,
                Title = "To Catch a Thief",
                ReleaseYear = 1955,
                Genres = new[] { Genre.Mystery, Genre.Thriller, Genre.Romance },
                RunningTime = new TimeSpan(1, 46, 0)
            };
            yield return new MovieModel
            {
                Id = counter++,
                Title = "It's a Wonderful Life",
                ReleaseYear = 1946,
                Genres = new[] { Genre.Drama, Genre.Family, Genre.Fantasy },
                RunningTime = new TimeSpan(2, 10, 0)
            };
            yield return new MovieModel
            {
                Id = counter++,
                Title = "Mr. Deeds Goes to Town",
                ReleaseYear = 1936,
                Genres = new[] { Genre.Comedy, Genre.Drama, Genre.Romance },
                RunningTime = new TimeSpan(1, 55, 0)
            };
            yield return new MovieModel
            {
                Id = counter++,
                Title = "Mr. Smith Goes to Washington",
                ReleaseYear = 1939,
                Genres = new[] { Genre.Comedy, Genre.Drama },
                RunningTime = new TimeSpan(2, 9, 0)
            };
            yield return new MovieModel
            {
                Id = counter++,
                Title = "The Shop Around the Corner",
                ReleaseYear = 1940,
                Genres = new[] { Genre.Comedy, Genre.Drama, Genre.Romance },
                RunningTime = new TimeSpan(1, 39, 0)
            };
            yield return new MovieModel
            {
                Id = counter++,
                Title = "Doctor Zhivago",
                ReleaseYear = 1965,
                Genres = new[] { Genre.Drama, Genre.Romance, Genre.War },
                RunningTime = new TimeSpan(3, 17, 0)
            };
            yield return new MovieModel
            {
                Id = counter++,
                Title = "Lawrence of Arabia",
                ReleaseYear = 1962,
                Genres = new[] { Genre.Adventure, Genre.Biography, Genre.Drama },
                RunningTime = new TimeSpan(3, 48, 0)
            };
            yield return new MovieModel
            {
                Id = counter++,
                Title = "You Can't Take It with You",
                ReleaseYear = 1938,
                Genres = new[] { Genre.Comedy, Genre.Drama, Genre.Romance },
                RunningTime = new TimeSpan(2, 6, 0)
            };
            yield return new MovieModel
            {
                Id = counter++,
                Title = "The Awful Truth",
                ReleaseYear = 1937,
                Genres = new[] { Genre.Comedy, Genre.Romance },
                RunningTime = new TimeSpan(1, 30, 0)
            };
            yield return new MovieModel
            {
                Id = counter++,
                Title = "Breakfast At Tiffany's",
                ReleaseYear = 1961,
                Genres = new[] { Genre.Comedy, Genre.Drama, Genre.Romance },
                RunningTime = new TimeSpan(1, 55, 0)
            };
            yield return new MovieModel
            {
                Id = counter++,
                Title = "The Artist",
                ReleaseYear = 2011,
                Genres = new[] { Genre.Comedy, Genre.Drama, Genre.Romance },
                RunningTime = new TimeSpan(1, 40, 0)
            };
            yield return new MovieModel
            {
                Id = counter++,
                Title = "Murder on the Orient Express",
                ReleaseYear = 1974,
                Genres = new[] { Genre.Crime, Genre.Drama, Genre.Mystery },
                RunningTime = new TimeSpan(2, 8, 0)
            };
            yield return new MovieModel
            {
                Id = counter++,
                Title = "The Treasure of the Sierra Madre",
                ReleaseYear = 1948,
                Genres = new[] { Genre.Adventure, Genre.Drama },
                RunningTime = new TimeSpan(2, 6, 0)
            };
            yield return new MovieModel
            {
                Id = counter++,
                Title = "The Big Sleep",
                ReleaseYear = 1946,
                Genres = new[] { Genre.Noir, Genre.Crime, Genre.Mystery },
                RunningTime = new TimeSpan(1, 54, 0)
            };
            yield return new MovieModel
            {
                Id = counter++,
                Title = "Casablanca",
                ReleaseYear = 1942,
                Genres = new[] { Genre.Drama, Genre.War, Genre.Romance },
                RunningTime = new TimeSpan(1, 42, 0)
            };
        }

        private static IEnumerable<RatingDto> SeedRatings()
        {
            var random = new Random();
            return
                from movie in _movies
                from user in _users
                select new RatingDto
                {
                    Id = random.Next(),
                    UserId = user.Id,
                    MovieId = movie.Id,
                    Score = random.Next(MinRating, MaxRating + 1)
                };
        }
    }
}
