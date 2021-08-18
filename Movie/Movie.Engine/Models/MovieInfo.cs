using System;
using System.Text.Json.Serialization;

namespace Movie.Engine.Models
{
    public class MovieInfo : IEquatable<MovieInfo>
    {
        [JsonPropertyName("id")]
        public long Id { get; init; }

        [JsonPropertyName("title")]
        public string Title { get; init; }

        [JsonPropertyName("yearOfRelease")]
        public int ReleaseYear { get; init; }

        [JsonPropertyName("runningTime")]
        public int RunningTime { get; init; }

        [JsonPropertyName("genres")]
        public string Genres { get; init; }

        [JsonPropertyName("averageRating")]
        public double AverageRating { get; init; }

        public override bool Equals(object other)
        {
            if (other == null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return other.GetType() == GetType() &&
                   Equals((MovieInfo) other);
        }

        public bool Equals(MovieInfo other)
        {
            if (other == null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Id == other.Id &&
                   Title.Equals(other.Title, StringComparison.InvariantCultureIgnoreCase) &&
                   ReleaseYear == other.ReleaseYear &&
                   RunningTime == other.RunningTime &&
                   Genres.Equals(other.Genres, StringComparison.InvariantCultureIgnoreCase) &&
                   AverageRating == other.AverageRating;
        }
    }
}
