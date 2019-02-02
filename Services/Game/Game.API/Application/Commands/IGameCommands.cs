using System.Collections.Generic;
using System.Threading.Tasks;
using Game.API.Models;
using Game.Domain.Entities;

namespace Game.API.Application.Commands
{
    /// <summary>
    /// Commands that make updates to a game.
    /// </summary>
    public interface IGameCommands
    {
        /// <summary>
        /// Shuffles the game's deck of cards.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IEnumerable<Card>> ShuffleDeckAsync(ShuffleDeckRequest request);

        /// <summary>
        /// Deals cards to players.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PokerGame> DealCardsAsync(DealCardsRequest request);

        /// <summary>
        /// Assigns each player in the round a score based on their hand values.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Round> RankRoundAsync(RankRoundRequest request);

        /// <summary>
        /// Adds a new round to the game.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IEnumerable<Round>> AppendRoundAsync(AppendRoundRequest request);
    }
}
