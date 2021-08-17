using System;
using System.Collections.Generic;
using System.Linq;
using Movie.Engine.Mappers;
using Movie.Engine.Models;
using Movie.Engine.Models.Dto;
using Movie.Engine.Models.Enums;
using Xunit;

namespace Movie.Engine.Tests.Mappers
{
    public class MovieInfoMapperClassTests
    {
        private readonly IMovieInfoMapper _sut = new MovieInfoMapper();

        [Theory]
        [MemberData(nameof(MapTestCases))]
        public void MapTests(RatedMovie ratedMovie, MovieInfo expected)
        {
            var actual = _sut.Map(ratedMovie);
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> MapTestCases()
        {
            // Test Simple Average
            yield return new object[]
            {
                new RatedMovie
                {
                    Movie = new MovieDto
                    {
                        Id = 1,
                        TitleName = "sms",
                        ReleaseYear = 1990,
                        RunningTime = new TimeSpan(1, 0, 0),
                        Genres = new Genre[] { Genre.Action, Genre.Fantasy, Genre.Thriller }
                    },
                    Ratings = new List<RatingDto>
                    {
                        new RatingDto { Score = 5 },
                        new RatingDto { Score = 4 },
                        new RatingDto { Score = 3 }
                    }
                },
                new MovieInfo
                {
                    Id = 1,
                    Title = "sms",
                    ReleaseYear = 1990,
                    RunningTime = 60,
                    Genres = "Action,Fantasy,Thriller",
                    AverageRating = 4
                }
            };

            // Test Averaging 2.91 to 3
            yield return new object[]
            {
                new RatedMovie
                {
                    Movie = new MovieDto
                    {
                        Id = 1,
                        TitleName = "sms",
                        ReleaseYear = 1990,
                        RunningTime = new TimeSpan(1, 0, 0),
                        Genres = new Genre[] { Genre.Action, Genre.Fantasy, Genre.Thriller }
                    },
                    Ratings = Enumerable
                        .Range(0, 10)
                        .Select(i => new RatingDto { Score = 3 })
                        .Append(new RatingDto {Score = 2})
                },
                new MovieInfo
                {
                    Id = 1,
                    Title = "sms",
                    ReleaseYear = 1990,
                    RunningTime = 60,
                    Genres = "Action,Fantasy,Thriller",
                    AverageRating = 3
                }
            };

            // Test Averaging 3.28 to 3.5
            yield return new object[]
            {
                new RatedMovie
                {
                    Movie = new MovieDto
                    {
                        Id = 1,
                        TitleName = "sms",
                        ReleaseYear = 1990,
                        RunningTime = new TimeSpan(1, 0, 0),
                        Genres = new Genre[] { Genre.Action, Genre.Fantasy, Genre.Thriller }
                    },
                    Ratings = Enumerable
                        .Range(0, 5)
                        .Select(i => new RatingDto { Score = 3 })
                        .Append(new RatingDto {Score = 4})
                        .Append(new RatingDto {Score = 4})
                },
                new MovieInfo
                {
                    Id = 1,
                    Title = "sms",
                    ReleaseYear = 1990,
                    RunningTime = 60,
                    Genres = "Action,Fantasy,Thriller",
                    AverageRating = 3.5
                }
            };

            // Test Averaging 3.625 to 3.5
            yield return new object[]
            {
                new RatedMovie
                {
                    Movie = new MovieDto
                    {
                        Id = 1,
                        TitleName = "sms",
                        ReleaseYear = 1990,
                        RunningTime = new TimeSpan(1, 0, 0),
                        Genres = new Genre[] { Genre.Action, Genre.Fantasy, Genre.Thriller }
                    },
                    Ratings = Enumerable
                        .Range(0, 5)
                        .Select(i => new RatingDto { Score = 3 })
                        .Append(new RatingDto {Score = 5})
                        .Append(new RatingDto {Score = 5})
                        .Append(new RatingDto {Score = 4})
                },
                new MovieInfo
                {
                    Id = 1,
                    Title = "sms",
                    ReleaseYear = 1990,
                    RunningTime = 60,
                    Genres = "Action,Fantasy,Thriller",
                    AverageRating = 3.5
                }
            };

            // Test Averaging 3.75 to 4
            yield return new object[]
            {
                new RatedMovie
                {
                    Movie = new MovieDto
                    {
                        Id = 1,
                        TitleName = "sms",
                        ReleaseYear = 1990,
                        RunningTime = new TimeSpan(1, 0, 0),
                        Genres = new Genre[] { Genre.Action, Genre.Fantasy, Genre.Thriller }
                    },
                    Ratings = Enumerable
                        .Range(0, 5)
                        .Select(i => new RatingDto { Score = 3 })
                        .Append(new RatingDto {Score = 5})
                        .Append(new RatingDto {Score = 5})
                        .Append(new RatingDto {Score = 5})
                },
                new MovieInfo
                {
                    Id = 1,
                    Title = "sms",
                    ReleaseYear = 1990,
                    RunningTime = 60,
                    Genres = "Action,Fantasy,Thriller",
                    AverageRating = 4
                }
            };

            // Test Minutes Addition
            yield return new object[]
            {
                new RatedMovie
                {
                    Movie = new MovieDto
                    {
                        Id = 1,
                        TitleName = "sms",
                        ReleaseYear = 1990,
                        RunningTime = new TimeSpan(1, 34, 0),
                        Genres = new Genre[] { Genre.Action, Genre.Fantasy, Genre.Thriller }
                    },
                    Ratings = Enumerable
                        .Range(0, 5)
                        .Select(i => new RatingDto { Score = 3 })
                        .Append(new RatingDto {Score = 5})
                        .Append(new RatingDto {Score = 5})
                        .Append(new RatingDto {Score = 5})
                },
                new MovieInfo
                {
                    Id = 1,
                    Title = "sms",
                    ReleaseYear = 1990,
                    RunningTime = 94,
                    Genres = "Action,Fantasy,Thriller",
                    AverageRating = 4
                }
            };
        }
    }
}
