using Microsoft.AspNetCore.Mvc;
using SF_API.Common;
using SF_API.DTOs.Game;
using SF_API.Interfaces;
using SF_API.Models;
using SF_API.Utils;

namespace SF_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGamesAsync()
        {
            ServiceResult<List<Game>> result = await _gameService.GetAllAsync();

            return StatusCode(result.Status, result);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameById(int id)
        {
            ServiceResult<Game> result = await _gameService.GetByIdAsync(id);

            return StatusCode(result.Status, result);

        }

        [HttpPost]
        public async Task<IActionResult> CreateGameAsync(CreateGameDTO createGame)
        {
            ServiceResult<Game> result = await _gameService.AddAsync(createGame);

            return StatusCode(result.Status, result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGameAsync(int id, [FromBody] CreateGameDTO updateGame)
        {
            ServiceResult<Game> result = await _gameService.UpdateAsync(id, updateGame);

            return StatusCode(result.Status, result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameAsync(int id)
        {
            ServiceResult<Game> result = await _gameService.DeleteAsync(id);

            return StatusCode(result.Status, result);

        }


    }
}
