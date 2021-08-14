using System;
using System.Text.Json.Serialization;

namespace Movie.Engine.Models
{
    public class MovieInfo
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("title")]
        public string Title { get; init; }

        [JsonPropertyName("yearOfRelease")]
        public int ReleaseYear { get; init; }

        [JsonPropertyName("runningTime")]
        public TimeSpan RunningTime { get; init; }

        [JsonPropertyName("genres")]
        public Genre[] Genres { get; init; }

        [JsonPropertyName("averageRating")]
        public float AverageRating { get; init; }
    }
}
