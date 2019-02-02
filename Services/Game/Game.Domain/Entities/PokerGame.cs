using System.Collections.Generic;

namespace Game.Domain.Entities
{
    public class PokerGame
    {
        public List<Card> Deck { get; set; } = new List<Card>();
        public List<Round> Rounds { get; set; } = new List<Round>();
    }
}
