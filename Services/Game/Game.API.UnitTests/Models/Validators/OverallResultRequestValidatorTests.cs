using System.Collections.Generic;
using System.Linq;
using Game.API.Models;
using Game.API.Models.Validators;
using Game.Domain.Entities;
using Xunit;

namespace Game.API.UnitTests.Models.Validators
{
    public class OverallResultRequestValidatorTests
    {
        private readonly OverallResultRequestValidator _validator = new OverallResultRequestValidator();

        [Fact]
        public void Valid_When_Game_Has_Rounds_And_Hands()
        {
            var request = SetupRequest(1, 1);
            
            var result = _validator.Validate(request);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Invalid_When_Game_Has_No_Rounds()
        {
            var request = SetupRequest(1, 1);
            request.PokerGame.Rounds =  new List<Round>();

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.True(result.Errors.First().ErrorMessage == "Rounds required");
        }

        [Fact]
        public void Invalid_When_Player_Has_No_Hand()
        {
            var request = SetupRequest(1, 1);
            request.PokerGame.Rounds.First().Players.First().Hand = null;

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.True(result.Errors.First().ErrorMessage == "Player hand required");
        }

        private static OverallResultRequest SetupRequest(int numRounds, int numPlayers)
        {
            var rounds = new List<Round>();

            for (var i = 0; i < numRounds; i++)
            {
                var round = new Round();

                for (var j = 0; j <= numPlayers; j++)
                {
                    round.Players.Add(new Player {Hand = new List<Card> {new Card()}});
                }

                rounds.Add(round);
            }

            return new OverallResultRequest {PokerGame = new PokerGame() {Rounds = rounds}};
        }
    }
}
