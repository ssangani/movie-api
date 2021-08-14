namespace Movie.Engine.Models.Dto
{
    public class RatingDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TitleId { get; set; }
        public int Score { get; set; }
    }
}
