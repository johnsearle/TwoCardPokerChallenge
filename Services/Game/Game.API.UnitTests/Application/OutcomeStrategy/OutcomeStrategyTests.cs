using System.Collections.Generic;
using Game.API.Application.OutcomeStrategy.Handlers;
using Game.Domain.Entities;
using Game.Domain.Entities.Enums;
using Xunit;

namespace Game.API.UnitTests.Application.OutcomeStrategy
{
    public class OutcomeStrategyTests
    {
        // TODO: Tests using remaining HandOutcomes.

        private readonly API.Application.OutcomeStrategy.OutcomeStrategy _outcomeStrategy;

        public OutcomeStrategyTests()
        {
            var handlers = new List<IOutcomeHandler>
            {
                new FlushHandler(),
                new HighCardHandler(),
                new PairHandler(),
                new StraightFlushHandler(),
                new StraightHandler()
            };
            _outcomeStrategy = new API.Application.OutcomeStrategy.OutcomeStrategy(handlers);
        }

        [Fact]
        public void Correct_Outcome_StraightFlush()
        {
            var hand = new List<Card>
            {
                new Card {Rank = Ranks.Two, Suit = Suits.Clubs},
                new Card {Rank = Ranks.Three, Suit = Suits.Clubs}
            };

            var result = _outcomeStrategy.HandOutcome(hand);

            Assert.True(result == HandOutcome.StraightFlush);
        }

        [Fact]
        public void Correct_Outcome_Pair()
        {
            var hand = new List<Card>
            {
                new Card {Rank = Ranks.Two, Suit = Suits.Clubs},
                new Card {Rank = Ranks.Two, Suit = Suits.Diamonds}
            };

            var result = _outcomeStrategy.HandOutcome(hand);

            Assert.True(result == HandOutcome.OnePair);
        }
    }
}
