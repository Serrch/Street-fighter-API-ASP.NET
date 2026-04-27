using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SF_API.Common;
using SF_API.DTOs.FighterMove;
using SF_API.Interfaces;
using SF_API.Models;
using SF_API.Utils;

namespace SF_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FighterMoveController : ControllerBase
    {
        private readonly IFighterMoveService _fighterMoveService;

        public FighterMoveController(IFighterMoveService fighterMoveService)
        {
            _fighterMoveService = fighterMoveService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetFighterMovesAsync()
        {
            ServiceResult<List<FighterMove>> result = await _fighterMoveService.GetAllAsync();

            return StatusCode(result.Status, result);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFighterMoveById(int id)
        {
            ServiceResult<FighterMove> result = await _fighterMoveService.GetByIdAsync(id);

            return StatusCode(result.Status, result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFighterMoveAsync(CreateFighterMoveDTO createFighterMove)
        {
            ServiceResult<FighterMove> result = await _fighterMoveService.AddAsync(createFighterMove);

            return StatusCode(result.Status, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFighterMoveAsync(int id, [FromBody] UpdateFighterMoveDTO updateFighterMove)
        {
            ServiceResult<FighterMove> result = await _fighterMoveService.UpdateAsync(id, updateFighterMove);

            return StatusCode(result.Status, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFighterMoveAsync(int id)
        {
            ServiceResult<FighterMove> result = await _fighterMoveService.DeleteAsync(id);

            return StatusCode(result.Status, result);
        }


    }
}
