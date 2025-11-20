using Microsoft.AspNetCore.Mvc;
using SF_API.Common;
using SF_API.DTOs.Fighter;
using SF_API.Interfaces;
using SF_API.Models;
using SF_API.Utils;

namespace SF_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FighterController : ControllerBase
    {
        private readonly IFighterService _fighterService;
        public FighterController(IFighterService fighterService)
        {
            _fighterService = fighterService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFightersAsync()
        {
            ServiceResult<List<Fighter>> result = await _fighterService.GetAllAsync();

            if (!result.Success) return NotFound(RespuestaFactory.Fail(result.Message, 404));

            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFighterById(int id)
        {
            ServiceResult<Fighter> result = await _fighterService.GetByIdAsync(id);

            if (!result.Success) return NotFound(RespuestaFactory.Fail(result.Message, 404));

            return Ok(RespuestaFactory.Ok(result.Message, result.Data));

        }

        [HttpPost]
        public async Task<IActionResult> CreateFighterAsync(CreateFighterDTO createFighter)
        {
            ServiceResult<Fighter> result = await _fighterService.AddAsync(createFighter);

            if (!result.Success) return BadRequest(RespuestaFactory.Fail(result.Message, 400));

            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFighterAsync(int id, [FromBody] CreateFighterDTO updateFighter)
        {
            ServiceResult<Fighter> result = await _fighterService.UpdateAsync(id, updateFighter);

            if (!result.Success)
            {
                if (result.ErrorType == ErrorType.NotFound) return NotFound(RespuestaFactory.Fail(result.Message, 404));

                return BadRequest(RespuestaFactory.Fail(result.Message, 400));

            }

            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFighterAsync(int id)
        {
            ServiceResult<Fighter> result = await _fighterService.DeleteAsync(id);
            if (!result.Success)
            {
                if(result.ErrorType == ErrorType.NotFound) return NotFound(RespuestaFactory.Fail(result.Message, 404));
                return BadRequest(RespuestaFactory.Fail(result.Message, 400));
            }
            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }

    }
}
