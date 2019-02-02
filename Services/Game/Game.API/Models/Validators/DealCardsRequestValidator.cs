using FluentValidation;

namespace Game.API.Models.Validators
{
    /// <summary>
    /// Ensure the request is valid.
    /// </summary>
    public class DealCardsRequestValidator : AbstractValidator<DealCardsRequest>
    {
        public DealCardsRequestValidator()
        {

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(request => request.PokerGame.Deck)
                .Must(x => x != null && x.Count > 0)
                .WithMessage("Game has no deck");

            RuleFor(request => request.PokerGame.Rounds)
                .Must(x => x != null && x.Count > 0)
                .WithMessage("Game has no rounds");
        }
    }
}
