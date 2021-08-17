using System;

namespace Movie.Engine.Models.Dto
{
    class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public TimeSpan RunningTime { get; set; }
        public string Genres { get; set; }
    }
}
