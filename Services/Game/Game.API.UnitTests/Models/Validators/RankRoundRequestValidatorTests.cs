using System.Collections.Generic;
using System.Linq;
using Game.API.Models;
using Game.API.Models.Validators;
using Game.Domain.Entities;
using Xunit;

namespace Game.API.UnitTests.Models.Validators
{
    public class RankRoundRequestValidatorTests
    {
        private readonly RankRoundRequestValidator _validator = new RankRoundRequestValidator();

        [Fact]
        public void Valid_When_Round_Has_Players_With_Hands()
        {
            var request = SetupRequest(1);

            var result = _validator.Validate(request);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Invalid_When_Round_Has_No_Players()
        {
            var request = SetupRequest(0);
            
            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.True(result.Errors.First().ErrorMessage == "Round has no players");
        }

        [Fact]
        public void Invalid_When_Player_Has_Empty_Hand()
        {
            var request = SetupRequest(1);
            request.Round.Players.First().Hand =  new List<Card>();

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.True(result.Errors.First().ErrorMessage == "Player has no hand");
        }

        [Fact]
        public void Invalid_When_Player_Has_Null_Hand()
        {
            var request = SetupRequest(1);
            request.Round.Players.First().Hand = null;

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.True(result.Errors.First().ErrorMessage == "Player has no hand");
        }


        private static RankRoundRequest SetupRequest(int numPlayers)
        {
            var players = new List<Player>();

            for (var i = 0; i < numPlayers; i++)
            {
                players.Add(new Player {Hand = new List<Card> { new Card() }});
            }

            var round = new Round {Players = players};

            return new RankRoundRequest { Round = round };
        }
    }
}
