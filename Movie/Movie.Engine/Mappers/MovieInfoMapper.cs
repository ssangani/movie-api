using System;
using System.Linq;
using Movie.Engine.Models;

namespace Movie.Engine.Mappers
{
    public class MovieInfoMapper : IMovieInfoMapper
    {
        public MovieInfo Map(RatedMovie from)
        {
            var avgScore = from.Ratings.Average(r => r.Score);
            return new MovieInfo
            {
                Id = from.Movie.Id,
                Title = from.Movie.TitleName,
                ReleaseYear = from.Movie.ReleaseYear,
                RunningTime = (int)from.Movie.RunningTime.TotalMinutes,
                Genres = string.Join(',', from.Movie.Genres),
                AverageRating = Math.Round(avgScore * 2, MidpointRounding.AwayFromZero) / 2
            };
        }
    }
}
