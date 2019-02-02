using System.Collections.Generic;
using Game.Domain.Entities;
using Game.Domain.Entities.Enums;
using Game.Domain.Entities.Extensions;

namespace Game.API.Application.OutcomeStrategy.Handlers
{
    public class FlushHandler : IOutcomeHandler
    {    
        public bool Handles(List<Card> hand)
        {
            return hand.IsFlush();
        }

        public HandOutcome Outcome()
        {
            return HandOutcome.Flush;
        }       
    }
}
