using System.Collections.Generic;
using System.Linq;
using Game.API.Models;
using Game.API.Models.Validators;
using Game.Domain.Entities;
using Xunit;

namespace Game.API.UnitTests.Models.Validators
{
    public class ShuffleDeckRequestValidatorTests
    {
        private readonly ShuffleDeckRequestValidator _validator = new ShuffleDeckRequestValidator();

        [Fact]
        public void Valid_When_Has_Deck()
        {
            var request = SetupRequest();

            var result = _validator.Validate(request);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Invalid_When_Null_Deck()
        {
            var request = SetupRequest();
            request.Deck = null;

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.True(result.Errors.First().ErrorMessage == "Valid deck required");
        }

        [Fact]
        public void Invalid_When_Deck_Empty()
        {
            var request = SetupRequest();
            request.Deck = new List<Card>();

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.True(result.Errors.First().ErrorMessage == "Valid deck required");
        }

        private static ShuffleDeckRequest SetupRequest()
        {
            return new ShuffleDeckRequest
            {
                Deck = new List<Card> {new Card()}
            };
        }
    }
}
