using System.Collections.Generic;
using System.Linq;
using Game.API.Application.Commands;
using Game.API.Application.OutcomeStrategy;
using Game.API.Models;
using Game.Domain.Entities;
using Game.Domain.Entities.Enums;
using Moq;
using Xunit;

namespace Game.API.UnitTests.Application.Commands
{
    public class GameCommandsTests
    {
        private readonly Mock<IOutcomeStrategy> _outcomeStrategy = new Mock<IOutcomeStrategy>();
        private readonly GameCommands _gameCommands;

        public GameCommandsTests()
        {
            _gameCommands = new GameCommands(_outcomeStrategy.Object);
        }

        [Fact]
        public void AppendRoundAsync_Clones_Players()
        {
            var players = new List<Player> {new Player {Id = 1}, new Player {Id = 2}};
            var rounds = new List<Round> {new Round {Players = players}};
            var request = new AppendRoundRequest {ExistingRounds = rounds};

            var result = _gameCommands.AppendRoundAsync(request).Result.ToList();

            Assert.True(result.Count == 2);
            Assert.True(result.Last().Players[0].Id == players[0].Id);
            Assert.True(result.Last().Players[1].Id == players[1].Id);
        }

        [Fact]
        public void ShuffleCardsAsync_Shuffles_Correctly()
        {
            var deck = new List<Card>
            {
                new Card {Rank = Ranks.Two, Suit = Suits.Clubs},
                new Card {Rank = Ranks.Three, Suit = Suits.Clubs}
            };

            for (var i = 0; i <= GameConstants.MaxReasonableAttemptsToShuffle; i++)
            {
                var request = new ShuffleDeckRequest { Deck = deck };

                var result = _gameCommands.ShuffleDeckAsync(request).Result.ToList();

                Assert.True(result[0].Weighting != deck[0].Weighting);

                deck = result;
            }
        }

        [Fact]
        public void RankRoundAsync_Ranks_Round_Correctly()
        {
            var player1 = new Player
            {
                Id = 1,
                Hand = new List<Card>
                {
                    new Card {Suit = Suits.Clubs, Rank = Ranks.Four}, new Card {Suit = Suits.Spades, Rank = Ranks.Queen}
                }
            };
            var player2 = new Player
            {
                Id = 2,
                Hand = new List<Card>
                    {new Card {Suit = Suits.Clubs, Rank = Ranks.Two}, new Card {Suit = Suits.Clubs, Rank = Ranks.Ace}}
            };
            var player3 = new Player
            {
                Id = 3,
                Hand = new List<Card>
                {
                    new Card {Suit = Suits.Hearts, Rank = Ranks.Two},
                    new Card {Suit = Suits.Diamonds, Rank = Ranks.Four}
                }
            };

            var players = new List<Player> {player1, player2, player3};
            var request = new RankRoundRequest {Round = new Round {Players = players}};

            _outcomeStrategy.Setup(x => x.HandOutcome(player1.Hand)).Returns(HandOutcome.Straight);
            _outcomeStrategy.Setup(x => x.HandOutcome(player2.Hand)).Returns(HandOutcome.Straight);
            _outcomeStrategy.Setup(x => x.HandOutcome(player3.Hand)).Returns(HandOutcome.Flush); // Highest outcome.

            var result = _gameCommands.RankRoundAsync(request).Result;

            // The lowest is player 2, because he shares a straight flush with 1, but his highest card is of a lower suit.
            var player = result.Players.Single(x => x.Id == 2);
            Assert.True(player.Score == 0);
            Assert.True(player.HighestCardWeighting == 312);

            // Higher is player 1, because he shares a straight flush with 2, but his highest card is of a higher suit.
            player = result.Players.Single(x => x.Id == 1);
            Assert.True(player.Score == 1);
            Assert.True(player.HighestCardWeighting == 410);

            // Highest of all is three, because even though his cards are low, he has a higher outcome (Flush).
            player = result.Players.Single(x => x.Id == 3);
            Assert.True(player.Score == 2);
            Assert.True(player.HighestCardWeighting == 200);
        }

        [Fact]
        public void DealCardsAsync_Deals_Correctly()
        {
            var deck = new List<Card>
            {
                new Card {Rank = Ranks.Two, Suit = Suits.Clubs},
                new Card {Rank = Ranks.Three, Suit = Suits.Clubs},
                new Card {Rank = Ranks.Four, Suit = Suits.Diamonds},
                new Card {Rank = Ranks.Five, Suit = Suits.Hearts}
            };

            var players = new List<Player> {new Player {Id = 1}, new Player {Id = 2}};
            var rounds = new List<Round> {new Round {Players = players}};

            var request = new DealCardsRequest {PokerGame = new PokerGame {Deck = deck, Rounds = rounds}};

            var result = _gameCommands.DealCardsAsync(request).Result;

            // Check player1 hand.
            var player1 = result.Rounds[0].Players.Single(x => x.Id == 1);
            Assert.True(player1.Hand[0].Rank == Ranks.Two && player1.Hand[0].Suit == Suits.Clubs);
            Assert.True(player1.Hand[1].Rank == Ranks.Four && player1.Hand[1].Suit == Suits.Diamonds);

            // Check player2 hand.
            var player2 = result.Rounds[0].Players.Single(x => x.Id == 2);
            Assert.True(player2.Hand[0].Rank == Ranks.Three && player2.Hand[0].Suit == Suits.Clubs);
            Assert.True(player2.Hand[1].Rank == Ranks.Five && player2.Hand[1].Suit == Suits.Hearts);

            // Check deck count reduced correctly.
            Assert.True(result.Deck.Count == 0);
        }
    }
}
