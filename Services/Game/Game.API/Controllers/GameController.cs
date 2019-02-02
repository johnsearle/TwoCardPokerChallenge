using System.Threading.Tasks;
using Game.API.Application.Commands;
using Game.API.Application.Queries;
using Game.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Game.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameQueries _queries;
        private readonly IGameCommands _commands;

        public GameController(IGameQueries gameQueries, IGameCommands gameCommands)
        {
            _queries = gameQueries;
            _commands = gameCommands;
        }

        [HttpPost("newGame")]
        public async Task<IActionResult> GetNewGame(NewGameRequest request)      
        {          
            var newGame = await _queries.GetNewGameAsync(request);
            return Ok(newGame);
        }

        [HttpPost("newRound")]
        public async Task<IActionResult> AppendRound(AppendRoundRequest request)
        {
            var rounds = await _commands.AppendRoundAsync(request);
            return Ok(rounds);
        }

        [HttpPost("shuffle")]
        public async Task<IActionResult> Shuffle(ShuffleDeckRequest request)
        {        
            var shuffledDeck = await _commands.ShuffleDeckAsync(request);
            return Ok(shuffledDeck);
        }

        [HttpPost("deal")]
        public async Task<IActionResult> Deal(DealCardsRequest request)
        {          
            var updatedGame = await _commands.DealCardsAsync(request);
            return Ok(updatedGame);  
        }

        [HttpPost("rank")]
        public async Task<IActionResult> Rank(RankRoundRequest request)
        {
            var updatedRound = await _commands.RankRoundAsync(request);
            return Ok(updatedRound);
        }

        [HttpPost("results")]
        public async Task<IActionResult> GetOverallResult(OverallResultRequest request)
        {
            var orderedGameResult = await _queries.GetOverallResultAsync(request);
            return Ok(orderedGameResult);
        }
    }
}