using Microsoft.AspNetCore.Mvc;
using SF_API.Common;
using SF_API.DTOs.FighterVersion;
using SF_API.Interfaces;
using SF_API.Models;
using SF_API.Utils;

namespace SF_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FighterVersionController : ControllerBase
    {
        private readonly IFighterVersionService _fighterVersionService;
        public FighterVersionController(IFighterVersionService fighterVersionService)
        {
            _fighterVersionService = fighterVersionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFighterVersionsAsync()
        {
            ServiceResult<List<FighterVersion>> result = await _fighterVersionService.GetAllAsync();

            if(!result.Success) return NotFound(RespuestaFactory.Fail(result.Message, 404));

            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFighterById(int id)
        {
            ServiceResult<FighterVersion> result = await _fighterVersionService.GetByIdAsync(id);

            if(!result.Success) return NotFound(RespuestaFactory.Fail(result.Message, 404));

            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFighterVersionAsync(CreateFighterVersionDTO createFighterVersion)
        {
            ServiceResult<FighterVersion> result = await _fighterVersionService.AddAsync(createFighterVersion);

            if (!result.Success) return BadRequest(RespuestaFactory.Fail(result.Message, 400));

            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFighterVersionAsync(int id, [FromBody] UpdateFighterVersionDTO updateFighter)
        {
            ServiceResult<FighterVersion> result = await _fighterVersionService.UpdateAsync(id, updateFighter);

            if (!result.Success)
            {
                if (result.ErrorType == ErrorType.NotFound) return NotFound();

                return BadRequest(RespuestaFactory.Fail(result.Message, 400));
            }
            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFighterVersionAsync(int id)
        {
            ServiceResult<FighterVersion> result = await _fighterVersionService.DeleteAsync(id);

            if (!result.Success)
            {
                if (result.ErrorType == ErrorType.NotFound) return NotFound(RespuestaFactory.Fail(result.Message, 404));

                return BadRequest(RespuestaFactory.Fail(result.Message, 400));
            }
            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }
    }
}
