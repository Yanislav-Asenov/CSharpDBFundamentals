namespace BookmakerSystem.Model
{
    using BookmakerSystem.Model.Attributes;

    public class ResultPrediction
    {
        public int Id { get; set; }

        [PredictionValidation]
        public string Prediction { get; set; }

        public int BetGameId { get; set; }

        public BetGame BetGame { get; set; }
    }
}
