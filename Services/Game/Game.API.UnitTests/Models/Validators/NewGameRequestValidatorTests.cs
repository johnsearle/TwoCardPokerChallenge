using System.Linq;
using Game.API.Models;
using Game.API.Models.Validators;
using Xunit;

namespace Game.API.UnitTests.Models.Validators
{
    public class NewGameRequestValidatorTests
    {
        private readonly NewGameRequestValidator _validator = new NewGameRequestValidator();
        
        [Fact]
        public void Valid_When_Num_Players_At_Range_Bottom()
        {
            var request = SetupRequest(GameConstants.MinPlayers);
           
            var result = _validator.Validate(request);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Valid_When_Num_Players_At_Range_Top()
        {
            var request = SetupRequest(GameConstants.MaxPlayers);

            var result = _validator.Validate(request);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Invalid_When_Num_Players_Too_Low()
        {
            var request = SetupRequest(GameConstants.MinPlayers - 1);
           
            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.True(result.Errors.First().ErrorMessage == $"Number of players must be between {GameConstants.MinPlayers} and {GameConstants.MaxPlayers}");
        }

        [Fact]
        public void Invalid_When_Num_Players_Too_High()
        {
            var request = SetupRequest(GameConstants.MaxPlayers + 1);

            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.True(result.Errors.First().ErrorMessage == $"Number of players must be between {GameConstants.MinPlayers} and {GameConstants.MaxPlayers}");
        }

        private static NewGameRequest SetupRequest(int numPlayers)
        {
            return new NewGameRequest { NumPlayers = numPlayers };
        }
    }
}
