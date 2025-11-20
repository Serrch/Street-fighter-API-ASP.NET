using Microsoft.AspNetCore.Mvc;
using SF_API.Common;
using SF_API.DTOs.Fighter;
using SF_API.DTOs.Game;
using SF_API.Interfaces;
using SF_API.Models;
using SF_API.Services;
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
            
            if(!result.Success) return NotFound(RespuestaFactory.Fail(result.Message, 404));

            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameById(int id)
        {
            ServiceResult<Game> result = await _gameService.GetByIdAsync(id);

            if (!result.Success) return NotFound(RespuestaFactory.Fail(result.Message, 404));

            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameAsync(CreateGameDTO createGame)
        {
            ServiceResult<Game> result = await _gameService.AddAsync(createGame);

            if (!result.Success) return BadRequest(RespuestaFactory.Fail(result.Message, 400));

            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGameAsync(int id, [FromBody] CreateGameDTO updateGame)
        {
            ServiceResult<Game> result = await _gameService.UpdateAsync(id, updateGame);

            if (!result.Success)
            {
                if (result.ErrorType == ErrorType.NotFound) return NotFound(RespuestaFactory.Fail(result.Message, 404));

                return BadRequest(RespuestaFactory.Fail(result.Message, 400));
            }

            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameAsync(int id)
        {
            ServiceResult<Game> result = await _gameService.DeleteAsync(id);
            if (!result.Success)
            {
                if (result.ErrorType == ErrorType.NotFound) return NotFound(RespuestaFactory.Fail(result.Message, 404));
                return BadRequest(RespuestaFactory.Fail(result.Message, 400));
            }
            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }


    }
}
