using System.Collections.Generic;
using System.Linq;
using Game.Domain.Entities.Enums;

namespace Game.Domain.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public List<Card> Hand { get; set; }
        public HandOutcome Outcome { get; set; }
        public int Score { get; set; }

        public int HighestCardWeighting
        {

            get
            {
                if (Hand != null && Hand.Any())
                {
                    return Hand.OrderBy(x => x.Weighting).Last().Weighting;
                }

                return 0;
            }
        }
    }
}
