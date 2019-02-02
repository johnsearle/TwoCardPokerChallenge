using System.Collections.Generic;
using System.Linq;
using Game.API.Models;
using Game.API.Models.Validators;
using Game.Domain.Entities;
using Xunit;

namespace Game.API.UnitTests.Models.Validators
{
    public class DealCardsRequestValidatorTests
    {
        private readonly DealCardsRequestValidator _validator = new DealCardsRequestValidator();
        
        [Fact]
        public void Invalid_When_Game_Has_Null_Deck()
        {
            var request = SetupRequest();
            request.PokerGame.Deck = null;

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.True(result.Errors.First().ErrorMessage == "Game has no deck");
        }

        [Fact]
        public void Invalid_When_Game_Has_Empty_Deck()
        {
            var request = SetupRequest();
            request.PokerGame.Deck = new List<Card>();

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.True(result.Errors.First().ErrorMessage == "Game has no deck");
        }

        [Fact]
        public void Invalid_When_Game_Has_Null_Rounds()
        {
            var request = SetupRequest();
            request.PokerGame.Rounds = null;

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.True(result.Errors.First().ErrorMessage == "Game has no rounds");
        }

        [Fact]
        public void Invalid_When_Game_Has_Empty_Rounds()
        {
            var request = SetupRequest();
            request.PokerGame.Rounds = new List<Round>();

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.True(result.Errors.First().ErrorMessage == "Game has no rounds");
        }

        private static DealCardsRequest SetupRequest()
        {
            return new DealCardsRequest
                {
                    PokerGame = new PokerGame
                    {
                       Deck = new List<Card> { new Card()},
                       Rounds = new List<Round> { new Round()}
                    }
                };
        }
    }
}
