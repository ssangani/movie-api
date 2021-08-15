using Movie.Engine.Models;

namespace Movie.Engine.Mappers
{
    public interface IMovieInfoMapper
    {
        MovieInfo Map(RatedMovie from);
    }
}
