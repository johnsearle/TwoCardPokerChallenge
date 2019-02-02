using System.Collections.Generic;
using Game.Domain.Entities;
using Game.Domain.Entities.Enums;

namespace Game.API.Application.OutcomeStrategy.Handlers
{
    public interface IOutcomeHandler
    {
        bool Handles(List<Card> hand);

        HandOutcome Outcome();
    }
}
