using System.Collections.Generic;
using Game.Domain.Entities;
using Game.Domain.Entities.Enums;
using Game.Domain.Entities.Extensions;

namespace Game.API.Application.OutcomeStrategy.Handlers
{
    public class StraightHandler : IOutcomeHandler
    {    
        public bool Handles(List<Card> hand)
        {
            return hand.IsStraight();
        }

        public HandOutcome Outcome()
        {
            return HandOutcome.Straight;
        }       
    }
}
