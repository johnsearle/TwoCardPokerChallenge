using System.Collections.Generic;

namespace Game.Domain.Entities
{
    public class Round
    {
        public List<Player> Players { get; set; } = new List<Player>();
    }
}
