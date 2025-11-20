using Microsoft.AspNetCore.Mvc;
using SF_API.Common;
using SF_API.DTOs.FighterMove;
using SF_API.Interfaces;
using SF_API.Models;
using SF_API.Utils;

namespace SF_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FighterMoveController : ControllerBase
    {
        private readonly IFighterMoveService _fighterMoveService;

        public FighterMoveController(IFighterMoveService fighterMoveService)
        {
            _fighterMoveService = fighterMoveService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFighterMovesAsync()
        {
            ServiceResult<List<FighterMove>> result = await _fighterMoveService.GetAllAsync();

            if (!result.Success) return NotFound(RespuestaFactory.Fail(result.Message, 404));

            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetFighterMoveById(int id)
        {
            ServiceResult<FighterMove> result = await _fighterMoveService.GetByIdAsync(id);

            if (!result.Success) return NotFound(RespuestaFactory.Fail(result.Message, 404));

            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFighterMoveAsync(CreateFighterMoveDTO createFighterMove)
        {
            ServiceResult<FighterMove> result = await _fighterMoveService.AddAsync(createFighterMove);

            if (!result.Success) return BadRequest(RespuestaFactory.Fail(result.Message, 400));

            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFighterMoveAsync(int id, [FromBody] UpdateFighterMoveDTO updateFighterMove)
        {
            ServiceResult<FighterMove> result = await _fighterMoveService.UpdateAsync(id, updateFighterMove);

            if (!result.Success)
            {
                if (result.ErrorType == ErrorType.NotFound) return NotFound();

                return BadRequest(RespuestaFactory.Fail(result.Message, 400));
            }
            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFighterMoveAsync(int id)
        {
            ServiceResult<FighterMove> result = await _fighterMoveService.DeleteAsync(id);

            if (!result.Success)
            {
                if (result.ErrorType == ErrorType.NotFound) return NotFound(RespuestaFactory.Fail(result.Message, 404));

                return BadRequest(RespuestaFactory.Fail(result.Message, 400));
            }
            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }


    }
}
