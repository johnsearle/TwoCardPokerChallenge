using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Game.API.Application.OutcomeStrategy;
using Game.API.Models;
using Game.Domain.Entities;
using MoreLinq;

namespace Game.API.Application.Commands
{
    /// <summary>
    /// Commands that make updates to a game.
    /// </summary>
    public class GameCommands : IGameCommands
    {
        private readonly IOutcomeStrategy _outcomeStrategy;

        /// <summary>
        /// Update PokerGame commands.
        /// </summary>
        /// <param name="outcomeStrategy"></param>
        public GameCommands(IOutcomeStrategy outcomeStrategy)
        {
            _outcomeStrategy = outcomeStrategy;
        }

        /// <summary>
        /// Shuffles the game's deck of cards.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Card>> ShuffleDeckAsync(ShuffleDeckRequest request)
        {
            var originalCardValues = request.Deck.Select(x => x.Weighting).ToList();

            var shuffleAttemptCount = 0;
            var shuffledDeck = new List<Card>();
            var shuffledOk = false;

            // It's possible a shuffle can result in the same sequence, so mitigate this by attempting
            // a reasonable number of times to get a different sequence.
            while (shuffleAttemptCount < GameConstants.MaxReasonableAttemptsToShuffle && !shuffledOk)
            {
                shuffledDeck = request.Deck.Shuffle().ToList();

                var newCardValues = shuffledDeck.Select(x => x.Weighting);

                shuffledOk = originalCardValues.SequenceEqual(newCardValues) == false;

                shuffleAttemptCount++;
                if (shuffleAttemptCount >= GameConstants.MaxReasonableAttemptsToShuffle)
                {
                    throw new ApplicationException($"Failed to shuffle within {GameConstants.MaxReasonableAttemptsToShuffle} attempts");
                }
            }

            return await Task.FromResult(shuffledDeck);
        }

        /// <summary>
        /// Deals cards to players.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PokerGame> DealCardsAsync(DealCardsRequest request)
        {
            var game = request.PokerGame;

            var latestRound = game.Rounds.Last();

            latestRound.Players.ForEach(x => x.Hand = new List<Card>());
          
            // Assign each play the next card in the deck, and remove that card
            //from the deck.
            for (var i = 0; i < GameConstants.NumCardsEach; i++)
            {
                foreach (var player in latestRound.Players)
                {
                    var card = game.Deck[0];
                    player.Hand.Add(card);
                    game.Deck.Remove(card);
                }
            }

            return await Task.FromResult(game);
        }

        /// <summary>
        /// Assigns each player in the round a score based on their hand values.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Round> RankRoundAsync(RankRoundRequest request)
        {
            var round = request.Round;

            round.Players.ForEach(x => x.Outcome = _outcomeStrategy.HandOutcome(x.Hand));          

            var orderedPlayers = round.Players.OrderBy(x => x.Outcome).ThenBy(x => x.HighestCardWeighting).ToList();

            for(var i = 0; i < orderedPlayers.Count(); i++)
            {
                var player = round.Players.Single(x => x.Id == orderedPlayers[i].Id);
                player.Score = i;
            }

            return await Task.FromResult(round);
        }

        /// <summary>
        /// Adds a new round to the game.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Round>> AppendRoundAsync(AppendRoundRequest request)
        {
            var rounds = request.ExistingRounds.ToList();
            var newRound = new Round();

            // Clone players from the previous round.
            foreach (var player in rounds.First().Players)
            {
                newRound.Players.Add(new Player { Id = player.Id });
            }

            rounds.Add(newRound);

            return await Task.FromResult(rounds);
        }
    }
}
