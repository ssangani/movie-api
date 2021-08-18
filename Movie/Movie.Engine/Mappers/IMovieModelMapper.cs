using System.Collections.Generic;
using Movie.Engine.Models;
using Movie.Engine.Models.Dto;

namespace Movie.Engine.Mappers
{
    public interface IMovieModelMapper
    {
        public IEnumerable<RatedMovie> Map(IEnumerable<MovieDto> movies, IEnumerable<RatingDto> movieRatings);
    }
}
