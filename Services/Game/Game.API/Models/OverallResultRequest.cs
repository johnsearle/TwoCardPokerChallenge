using Game.Domain.Entities;

namespace Game.API.Models
{
    public class OverallResultRequest
    {
        /// <summary>
        /// The game from which a result will be determined.
        /// </summary>
        public PokerGame PokerGame { get; set; }
    }
}
