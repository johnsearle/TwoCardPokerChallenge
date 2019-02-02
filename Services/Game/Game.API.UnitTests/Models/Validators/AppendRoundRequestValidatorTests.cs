using System.Collections.Generic;
using System.Linq;
using Game.API.Models;
using Game.API.Models.Validators;
using Game.Domain.Entities;
using Xunit;

namespace Game.API.UnitTests.Models.Validators
{
    public class AppendRoundRequestValidatorTests
    {
        private readonly AppendRoundRequestValidator _validator = new AppendRoundRequestValidator();

        [Fact]
        public void Valid_When_Existing_Rounds_Less_Than_Max()
        {
            var request = SetupRequest(GameConstants.MaxRounds - 1);
            
            var result = _validator.Validate(request);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Invalid_When_Existing_Rounds_Equals_Max()
        {
            var request = SetupRequest(GameConstants.MaxRounds);
          
            var result = _validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count == 1);
            Assert.True(result.Errors.First().ErrorMessage == $"Number of rounds cannot exceed {GameConstants.MaxRounds}");
        }

        private static AppendRoundRequest SetupRequest(int numRounds)
        {
            var existingRounds = new List<Round>();

            for (var i = 0; i < numRounds; i++)
            {
                existingRounds.Add(new Round());
            }

            return new AppendRoundRequest { ExistingRounds = existingRounds };
        }
    }
}
