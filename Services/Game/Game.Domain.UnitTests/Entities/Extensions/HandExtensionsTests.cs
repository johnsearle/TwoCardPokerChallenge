using Game.Domain.Entities;
using System;
using System.Collections.Generic;
using Game.Domain.Entities.Enums;
using Game.Domain.Entities.Extensions;
using Xunit;

namespace Game.Domain.UnitTests
{
    public class HandExtensionsTests
    {

        // TODO: Tests on IsPair, IsHighCard.

        [Fact]
        public void IsStraightFlush_Correct()
        {
            var hand = new List<Card> {
                new Card { Rank = Ranks.Two, Suit = Suits.Clubs },
                new Card { Rank = Ranks.Three, Suit = Suits.Clubs }
            };

            Assert.True(hand.IsStraightFlush());
            Assert.False(hand.IsFlush());
            Assert.False(hand.IsStraight());
            Assert.False(hand.IsPair());
            Assert.False(hand.IsHighCard());
        }

        [Fact]
        public void IsFlush_Correct()
        {
            var hand = new List<Card> {
                new Card { Rank = Ranks.Two, Suit = Suits.Clubs },
                new Card { Rank = Ranks.Four, Suit = Suits.Clubs }
            };

            Assert.False(hand.IsStraightFlush());
            Assert.True(hand.IsFlush());
            Assert.False(hand.IsStraight());
            Assert.False(hand.IsPair());
            Assert.False(hand.IsHighCard());
        }

        [Fact]
        public void IsStraight_Correct()
        {
            var hand = new List<Card> {
                new Card { Rank = Ranks.Two, Suit = Suits.Clubs },
                new Card { Rank = Ranks.Three, Suit = Suits.Diamonds }
            };

            Assert.False(hand.IsStraightFlush());
            Assert.False(hand.IsFlush());
            Assert.True(hand.IsStraight());
            Assert.False(hand.IsPair());
            Assert.False(hand.IsHighCard());
        }
    }
}
