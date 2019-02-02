using System.Linq;
using FluentValidation;

namespace Game.API.Models.Validators
{
    /// <summary>
    /// Ensure the request is valid.
    /// </summary>
    public class ShuffleDeckRequestValidator : AbstractValidator<ShuffleDeckRequest>
    {
        public ShuffleDeckRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(request => request.Deck)
                .Must(x => x != null && x.Any())
                .WithMessage("Valid deck required");
        }
    }
}
