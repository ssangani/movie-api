namespace Movie.Engine.Models.Dto
{
    public class RatingDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long MovieId { get; set; }
        public int Score { get; set; }
    }
}
