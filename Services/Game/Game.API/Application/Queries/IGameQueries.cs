using System.Collections.Generic;
using System.Threading.Tasks;
using Game.API.Models;
using Game.Domain.Entities;

namespace Game.API.Application.Queries
{
    /// <summary>
    /// Operations that return a game, but do not update state.
    /// </summary>
    public interface IGameQueries
    {
        /// <summary>
        /// Instantiate a new game.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PokerGame> GetNewGameAsync(NewGameRequest request);

        /// <summary>
        /// Get the results of a game.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IEnumerable<OverallResult>> GetOverallResultAsync(OverallResultRequest request);
    }   
}
