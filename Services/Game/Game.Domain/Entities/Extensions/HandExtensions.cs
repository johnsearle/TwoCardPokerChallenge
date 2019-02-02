using System.Collections.Generic;

namespace Game.Domain.Entities.Extensions
{
    /// <summary>
    /// Extensions for a list of cards (Hand/Deck).
    /// </summary>
    public static class HandExtensions
    {   
        public static bool IsStraightFlush(this List<Card> hand)
        {
            return hand.IsSameSuit() && hand.IsSequentialRank();
        } 

        public static bool IsFlush(this List<Card> hand)
        {
            return hand.IsSameSuit() && !hand.IsSequentialRank();
        }

        public static bool IsStraight(this List<Card> hand)
        {
            return !hand.IsSameSuit() && hand.IsSequentialRank();
        }

        public static bool IsPair(this List<Card> hand)
        {
            return !hand.IsSameSuit() && hand.IsSameRank();
        }

        public static bool IsHighCard(this List<Card> hand)
        {
            return !hand.IsSameRank() && !hand.IsSameSuit() && !hand.IsSequentialRank();
        }

        private static bool IsSameSuit(this List<Card> hand)
        {
            //TODO: Assert count == 2.

            return hand[0].Suit == hand[1].Suit;
        }

        private static bool IsSameRank(this List<Card> hand)
        {
            //TODO: Assert count == 2.

            return hand[0].Rank == hand[1].Rank;
        }

        private static bool IsSequentialRank(this List<Card> hand)
        {
            //TODO: Assert count == 2.

            return hand[0].Rank == hand[1].Rank + 1 || hand[0].Rank == hand[1].Rank - 1;
        }
    }
}
