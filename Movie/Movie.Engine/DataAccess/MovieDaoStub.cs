﻿using System;
using System.Collections.Generic;
using System.Linq;
using Movie.Engine.Models.Dto;
using Movie.Engine.Models.Enums;

namespace Movie.Engine.DataAccess
{
    public class MovieDaoStub : IMovieDao
    {
        private const int MinRating = 1;
        private const int MaxRating = 5;
        private static readonly List<UserDto> _users;
        private static readonly List<MovieDto> _movies;
        private static readonly List<RatingDto> _ratings;

        static MovieDaoStub()
        {
            _users = SeedUsers().ToList();
            _movies = SeedMovies().ToList();
            _ratings = SeedRatings().ToList();
        }

        private IEnumerable<MovieDto> GetMovies(string title, int? yearOfRelease, IEnumerable<Genre> genres)
        {
            var noTitleSpecified = string.IsNullOrWhiteSpace(title);
            var noYearSpecified = !yearOfRelease.HasValue;
            var noGenresSpecified = genres.Count() == 0;
            return _movies
                .Where(m => noTitleSpecified || m.TitleName.Contains(title, StringComparison.InvariantCultureIgnoreCase))
                .Where(m => noYearSpecified || m.ReleaseYear == yearOfRelease.Value)
                .Where(m => noGenresSpecified || genres.All(genre => m.Genres.Contains(genre)));
        }

        private IEnumerable<RatingDto> GetRatingsByTitle(IEnumerable<int> titleIds)
        {
            var titles = new HashSet<int>(titleIds);
            return _ratings
                .Where(r => titles.Contains(r.TitleId));
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

        private static IEnumerable<MovieDto> SeedMovies()
        {
            int counter = 1;
            yield return new MovieDto {
                Id = counter++,
                TitleName = "My Man Godfrey",
                ReleaseYear = 1936,
                Genres = new[] { Genre.Comedy, Genre.Drama, Genre.Romance },
                RunningTime = new TimeSpan(1, 34, 0) 
            };
            yield return new MovieDto
            {
                Id = counter++,
                TitleName = "It Happened One Night",
                ReleaseYear = 1934,
                Genres = new[] { Genre.Comedy, Genre.Romance },
                RunningTime = new TimeSpan(1, 45, 0)
            };
            yield return new MovieDto
            {
                Id = counter++,
                TitleName = "The Apartment",
                ReleaseYear = 1960,
                Genres = new[] { Genre.Comedy, Genre.Drama, Genre.Romance },
                RunningTime = new TimeSpan(2, 5, 0)
            };
            yield return new MovieDto
            {
                Id = counter++,
                TitleName = "How to Steal a Million",
                ReleaseYear = 1966,
                Genres = new[] { Genre.Comedy, Genre.Crime, Genre.Romance },
                RunningTime = new TimeSpan(2, 3, 0)
            };
            yield return new MovieDto
            {
                Id = counter++,
                TitleName = "To Catch a Thief",
                ReleaseYear = 1955,
                Genres = new[] { Genre.Mystery, Genre.Thriller, Genre.Romance },
                RunningTime = new TimeSpan(1, 46, 0)
            };
            yield return new MovieDto
            {
                Id = counter++,
                TitleName = "It's a Wonderful Life",
                ReleaseYear = 1946,
                Genres = new[] { Genre.Drama, Genre.Family, Genre.Fantasy },
                RunningTime = new TimeSpan(2, 10, 0)
            };
            yield return new MovieDto
            {
                Id = counter++,
                TitleName = "Mr. Deeds Goes to Town",
                ReleaseYear = 1936,
                Genres = new[] { Genre.Comedy, Genre.Drama, Genre.Romance },
                RunningTime = new TimeSpan(1, 55, 0)
            };
            yield return new MovieDto
            {
                Id = counter++,
                TitleName = "Mr. Smith Goes to Washington",
                ReleaseYear = 1939,
                Genres = new[] { Genre.Comedy, Genre.Drama },
                RunningTime = new TimeSpan(2, 9, 0)
            };
            yield return new MovieDto
            {
                Id = counter++,
                TitleName = "The Shop Around the Corner",
                ReleaseYear = 1940,
                Genres = new[] { Genre.Comedy, Genre.Drama, Genre.Romance },
                RunningTime = new TimeSpan(1, 39, 0)
            };
            yield return new MovieDto
            {
                Id = counter++,
                TitleName = "Doctor Zhivago",
                ReleaseYear = 1965,
                Genres = new[] { Genre.Drama, Genre.Romance, Genre.War },
                RunningTime = new TimeSpan(3, 17, 0)
            };
            yield return new MovieDto
            {
                Id = counter++,
                TitleName = "Lawrence of Arabia",
                ReleaseYear = 1962,
                Genres = new[] { Genre.Adventure, Genre.Biography, Genre.Drama },
                RunningTime = new TimeSpan(3, 48, 0)
            };
            yield return new MovieDto
            {
                Id = counter++,
                TitleName = "You Can't Take It with You",
                ReleaseYear = 1938,
                Genres = new[] { Genre.Comedy, Genre.Drama, Genre.Romance },
                RunningTime = new TimeSpan(2, 6, 0)
            };
            yield return new MovieDto
            {
                Id = counter++,
                TitleName = "The Awful Truth",
                ReleaseYear = 1937,
                Genres = new[] { Genre.Comedy, Genre.Romance },
                RunningTime = new TimeSpan(1, 30, 0)
            };
            yield return new MovieDto
            {
                Id = counter++,
                TitleName = "Breakfast At Tiffany's",
                ReleaseYear = 1961,
                Genres = new[] { Genre.Comedy, Genre.Drama, Genre.Romance },
                RunningTime = new TimeSpan(1, 55, 0)
            };
            yield return new MovieDto
            {
                Id = counter++,
                TitleName = "The Artist",
                ReleaseYear = 2011,
                Genres = new[] { Genre.Comedy, Genre.Drama, Genre.Romance },
                RunningTime = new TimeSpan(1, 40, 0)
            };
            yield return new MovieDto
            {
                Id = counter++,
                TitleName = "Murder on the Orient Express",
                ReleaseYear = 1974,
                Genres = new[] { Genre.Crime, Genre.Drama, Genre.Mystery },
                RunningTime = new TimeSpan(2, 8, 0)
            };
            yield return new MovieDto
            {
                Id = counter++,
                TitleName = "The Treasure of the Sierra Madre",
                ReleaseYear = 1948,
                Genres = new[] { Genre.Adventure, Genre.Drama },
                RunningTime = new TimeSpan(2, 6, 0)
            };
            yield return new MovieDto
            {
                Id = counter++,
                TitleName = "The Big Sleep",
                ReleaseYear = 1946,
                Genres = new[] { Genre.Noir, Genre.Crime, Genre.Mystery },
                RunningTime = new TimeSpan(1, 54, 0)
            };
            yield return new MovieDto
            {
                Id = counter++,
                TitleName = "Casablanca",
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
                    TitleId = movie.Id,
                    Score = random.Next(MinRating, MaxRating + 1)
                };
        }
    }
}
