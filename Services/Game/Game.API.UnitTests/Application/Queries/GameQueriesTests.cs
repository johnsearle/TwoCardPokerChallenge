using System;
using System.Collections.Generic;
using System.Linq;
using Game.API.Application.Queries;
using Game.API.Models;
using Game.Domain.Entities;
using Game.Domain.Entities.Enums;
using Xunit;

namespace Game.API.UnitTests.Application.Queries
{
    public class GameQueriesTests
    {
        private readonly GameQueries _gameQueries;

        public GameQueriesTests()
        {
            _gameQueries =  new GameQueries();
        }

        [Fact]
        public void GetNewGameAsync_Returns_Complete_Deck()
        {
            var request = new NewGameRequest { NumPlayers = 3 };

            var result = _gameQueries.GetNewGameAsync(request).Result;

            var count = 0;

            foreach (var suitValue in Enum.GetNames(typeof(Suits)))
            {
                foreach (var rankValue in Enum.GetNames(typeof(Ranks)))
                {
                    var card = result.Deck.SingleOrDefault(x => x.Suit.ToString() == suitValue && x.Rank.ToString() == rankValue);
                    if (card != null)
                    {
                        count++;
                    }
                }
            }

            Assert.True(count == 52);
        }

        [Fact]
        public void GetOverallResultAsync_Returns_Correct_Result()
        {
            var round1 = new Round
            {
                Players = new List<Player> {new Player { Id = 1, Score = 1 }, new Player { Id = 2, Score = 4 } }
            };

            var round2 = new Round
            {
                Players = new List<Player> { new Player { Id = 1, Score = 2 }, new Player { Id = 2, Score = 5 } }
            };

            var game = new PokerGame { Rounds = new List<Round> { round1, round2 } };
            var request = new OverallResultRequest { PokerGame = game };

            var result = _gameQueries.GetOverallResultAsync(request).Result.ToList();

            var firstResult = result.First();
            var lastResult = result.Last();

            Assert.True(firstResult.PlayerId == 2 && firstResult.OverallScore == 9);
            Assert.True(lastResult.PlayerId == 1 && lastResult.OverallScore == 3);
        }
    }
}
