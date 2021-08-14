using System;
using System.Collections.Generic;
using System.Linq;
using Movie.Engine.Models.Dto;
using Movie.Engine.Models.Enums;

namespace Movie.Engine.DataAccess
{
    class MovieDaoStub : IMovieDao
    {
        private static readonly List<UserDto> _users;
        private static readonly List<MovieDto> _movies;
        private static readonly List<RatingDto> _ratings;

        static MovieDaoStub()
        {
            _users = SeedUsers().ToList();
            _movies = SeedMovies().ToList();
            _ratings = SeedRatings().ToList();
        }

        private static IEnumerable<UserDto> SeedUsers()
        {
            yield return new UserDto { Id = 1, Username = "farley" };
            yield return new UserDto { Id = 2, Username = "jakob" };
        }

        private static IEnumerable<MovieDto> SeedMovies()
        {
            yield return new MovieDto {
                Id = 1,
                TitleName = "My Man Godfrey",
                ReleaseYear = 1936,
                Genres = new[] { Genre.Comedy, Genre.Drama, Genre.Romance },
                RunningTime = new TimeSpan(1, 34, 0) 
            };
        }

        private static IEnumerable<RatingDto> SeedRatings()
        {
            yield return new RatingDto
            {
                Id = 1,
                TitleId = 1,
                UserId = 1,
                Score = 5
            };
        }
    }
}
