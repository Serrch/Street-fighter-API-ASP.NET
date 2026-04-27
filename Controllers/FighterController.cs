using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SF_API.Common;
using SF_API.DTOs.Fighter;
using SF_API.Interfaces;
using SF_API.Models;

namespace SF_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FighterController : ControllerBase
    {
        private readonly IFighterService _fighterService;
        public FighterController(IFighterService fighterService)
        {
            _fighterService = fighterService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetFightersAsync()
        {
            ServiceResult<List<Fighter>> result = await _fighterService.GetAllAsync();

            return StatusCode(result.Status, result);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFighterById(int id)
        {
            ServiceResult<Fighter> result = await _fighterService.GetByIdAsync(id);

            return StatusCode(result.Status, result);

        }

        [HttpPost]
        public async Task<IActionResult> CreateFighterAsync(CreateFighterDTO createFighter)
        {
            ServiceResult<Fighter> result = await _fighterService.AddAsync(createFighter);

            return StatusCode(result.Status, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFighterAsync(int id, [FromBody] CreateFighterDTO updateFighter)
        {
            ServiceResult<Fighter> result = await _fighterService.UpdateAsync(id, updateFighter);

            return StatusCode(result.Status, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFighterAsync(int id)
        {
            ServiceResult<Fighter> result = await _fighterService.DeleteAsync(id);
            return StatusCode(result.Status, result);
        }

    }
}
