using System;
using Movie.Engine.Models.Enums;

namespace Movie.Engine.Models
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string TitleName { get; set; }
        public int ReleaseYear { get; set; }
        public Genre[] Genres { get; set; }
        public TimeSpan RunningTime { get; set; }
    }
}
