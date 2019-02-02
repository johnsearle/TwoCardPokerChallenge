using System.Linq;
using FluentValidation;

namespace Game.API.Models.Validators
{
    /// <summary>
    /// Ensure the request is valid.
    /// </summary>
    public class OverallResultRequestValidator : AbstractValidator<OverallResultRequest>
    {
        public OverallResultRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(request => request.PokerGame.Rounds)
                .Must(x => x != null && x.Any())
                .WithMessage("Rounds required");

            RuleFor(request => request.PokerGame.Rounds)
                .Must(r => r.All(p => p.Players.All(h => h.Hand != null && h.Hand.Count > 0)))
                .WithMessage("Player hand required");
        }
    }
}
