using System.Collections.Generic;
using Game.Domain.Entities;

namespace Game.API.Models
{
    public class ShuffleDeckRequest
    {
        /// <summary>
        /// The deck of cards to be shuffled.
        /// </summary>
        public IEnumerable<Card> Deck { get; set; }
    }
}
