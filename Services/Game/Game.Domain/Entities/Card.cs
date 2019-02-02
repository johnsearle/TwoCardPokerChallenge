using Game.Domain.Entities.Enums;

namespace Game.Domain.Entities
{
    public class Card
    {
        public Suits Suit { get; set; }
        public Ranks Rank { get; set; }

        public int Weighting => (int)Suit + (int)Rank;
    }
}
