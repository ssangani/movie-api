using System;
using System.Collections.Generic;
using System.Linq;
using Movie.Engine.Models;
using Movie.Engine.Models.Dto;
using Movie.Engine.Models.Enums;

namespace Movie.Engine.Mappers
{
    public class MovieModelMapper : IMovieModelMapper
    {
        public IEnumerable<RatedMovie> Map(IEnumerable<MovieDto> movies, IEnumerable<RatingDto> movieRatings)
        {
            var movieRatingsLookup = movieRatings
                .GroupBy(rating => rating.MovieId, rating => rating)
                .ToDictionary(movieGroup => movieGroup.Key, movieGroup => movieGroup.Select(r => r));

            return movies
                .Select(movie => new RatedMovie
                {
                    Movie = new MovieModel
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                        RunningTime = movie.RunningTime,
                        ReleaseYear = movie.ReleaseYear,
                        Genres = Parse(movie.Genres),
                    },
                    Ratings = movieRatingsLookup.TryGetValue(movie.Id, out var ratings) ? ratings : Enumerable.Empty<RatingDto>()
                });
        }

        private Genre[] Parse(string genres)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(genres))
                {
                    return new Genre[0];
                }


                return genres
                    .Split(",")
                    .Select(genreId => int.TryParse(genreId, out int g) ? g : 0)
                    .Where(g => g != 0)
                    .Select(g => (Genre)g)
                    .ToArray();
            }
            catch (Exception e)
            {

                return new Genre[0];
            }
        }
    }
}
