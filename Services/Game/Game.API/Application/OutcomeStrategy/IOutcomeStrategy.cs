using System.Collections.Generic;
using Game.Domain.Entities;
using Game.Domain.Entities.Enums;

namespace Game.API.Application.OutcomeStrategy
{
    /// <summary>
    /// Strategy pattern for calculating hand outcomes.
    /// </summary>
    public interface IOutcomeStrategy
    {
        HandOutcome HandOutcome(List<Card> hand);
    }
}
