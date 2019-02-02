using System.Linq;
using FluentValidation;

namespace Game.API.Models.Validators
{
    /// <summary>
    /// Ensure the request is valid.
    /// </summary>
    public class RankRoundRequestValidator : AbstractValidator<RankRoundRequest>
    {
        public RankRoundRequestValidator()
        {

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(request => request.Round)
                .Must(x => x.Players != null && x.Players.Count > 0)
                .WithMessage("Round has no players");

            RuleFor(request => request.Round)
                .Must(x => x.Players.All(y => y.Hand != null && y.Hand.Count > 0))
                .WithMessage("Player has no hand");
        }
    }
}
