using Game.Domain.Entities;

namespace Game.API.Models
{
    public class RankRoundRequest
    {
        /// <summary>
        /// The round in which player's hands will be ranked.
        /// </summary>
        public Round Round { get; set; }
    }
}
