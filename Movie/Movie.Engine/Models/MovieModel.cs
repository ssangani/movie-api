using System;
using Movie.Engine.Models.Enums;

namespace Movie.Engine.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public Genre[] Genres { get; set; }
        public TimeSpan RunningTime { get; set; }
    }
}
