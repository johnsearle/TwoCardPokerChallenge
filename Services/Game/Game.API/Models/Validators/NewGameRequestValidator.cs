using System.Linq;
using FluentValidation;

namespace Game.API.Models.Validators
{
    /// <summary>
    /// Ensure the request is valid.
    /// </summary>
    public class NewGameRequestValidator : AbstractValidator<NewGameRequest>
    {
        public NewGameRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(request => request.NumPlayers)
                .Must(x => x >= GameConstants.MinPlayers && x <= GameConstants.MaxPlayers)
                .WithMessage($"Number of players must be between {GameConstants.MinPlayers} and {GameConstants.MaxPlayers}");
        }
    }
}
