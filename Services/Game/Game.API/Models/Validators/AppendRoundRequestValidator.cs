using System.Linq;
using FluentValidation;

namespace Game.API.Models.Validators
{
    /// <summary>
    /// Ensure the request is valid.
    /// </summary>
    public class AppendRoundRequestValidator : AbstractValidator<AppendRoundRequest>
    {
        public AppendRoundRequestValidator()
        {
            RuleFor(request => request.ExistingRounds)
                .Must(x => x.Count() < GameConstants.MaxRounds)
                .WithMessage($"Number of rounds cannot exceed {GameConstants.MaxRounds}");
        }
    }
}
