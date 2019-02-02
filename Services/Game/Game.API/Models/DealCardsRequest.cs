using Game.Domain.Entities;

namespace Game.API.Models
{
   
    public class DealCardsRequest
    {
        /// <summary>
        /// The game for which cards will be dealt to players.
        /// </summary>
        public PokerGame PokerGame { get; set; }
    }
}
