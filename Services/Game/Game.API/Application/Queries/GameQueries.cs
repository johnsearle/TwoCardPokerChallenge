using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Game.API.Models;
using Game.Domain.Entities;
using Game.Domain.Entities.Enums;

namespace Game.API.Application.Queries
{
    /// <summary>
    /// Operations that return a game, but do not update state.
    /// </summary>
    public class GameQueries : IGameQueries
    {
        /// <summary>
        /// Instantiate a new game.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PokerGame> GetNewGameAsync(NewGameRequest request)
        {
            var game = new PokerGame();

            // Generate a new deck.
            foreach (var suitValue in Enum.GetValues(typeof(Suits)))
            {
                foreach (var rankValue in Enum.GetValues(typeof(Ranks)))
                {
                    game.Deck.Add(new Card { Suit = (Suits)suitValue, Rank = (Ranks)rankValue });
                }
            }

            // Generate a list of players.
            var round = new Round();
            for (var i = 0; i < request.NumPlayers; i++)
            {
                round.Players.Add(new Player { Id = i + 1 });
            }

            game.Rounds.Add(round);           

            return await Task.FromResult(game);
        }

        /// <summary>
        /// Get the results of a game.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IEnumerable<OverallResult>> GetOverallResultAsync(OverallResultRequest request)
        {   
            // Sum player's scores for each round.
            var result = from player in request.PokerGame.Rounds.SelectMany(x => x.Players)
                    group player by player.Id into grp
                    select new OverallResult { PlayerId = grp.First().Id, OverallScore = grp.Sum(p => p.Score )}; 

            // Return the result ordered with the winner(s) at the top.
            return await Task.FromResult(result.OrderByDescending(x => x.OverallScore));
        }
    }
}
