using System.Collections.Generic;
using Game.Domain.Entities;

namespace Game.API.Models
{
    public class AppendRoundRequest
    {
        /// <summary>
        /// The set of existing rounds.
        /// </summary>
        public IEnumerable<Round> ExistingRounds { get; set; }
    }
}
