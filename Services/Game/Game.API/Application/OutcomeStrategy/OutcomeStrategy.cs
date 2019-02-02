using System;
using System.Collections.Generic;
using System.Linq;
using Game.API.Application.OutcomeStrategy.Handlers;
using Game.Domain.Entities;
using Game.Domain.Entities.Enums;

namespace Game.API.Application.OutcomeStrategy
{
    /// <summary>
    /// Strategy pattern for calculating hand outcomes.
    /// </summary>
    public class OutcomeStrategy : IOutcomeStrategy
    {
        private readonly IEnumerable<IOutcomeHandler> _handlers;

        public OutcomeStrategy(IEnumerable<IOutcomeHandler> handlers)
        {
            _handlers = handlers;
        }

        /// <summary>
        /// Get the outcome of the first one that handles this type of hand.
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public HandOutcome HandOutcome(List<Card> hand)
        {
            return _handlers.FirstOrDefault(x => x.Handles(hand))?.Outcome() ?? throw new ArgumentNullException(nameof(hand));
        }
    }
}
