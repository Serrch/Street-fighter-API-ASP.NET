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

            return StatusCode(result.Status, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFighterById(int id)
        {
            ServiceResult<FighterVersion> result = await _fighterVersionService.GetByIdAsync(id);

            return StatusCode(result.Status, result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFighterVersionAsync(CreateFighterVersionDTO createFighterVersion)
        {
            ServiceResult<FighterVersion> result = await _fighterVersionService.AddAsync(createFighterVersion);

            return StatusCode(result.Status, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFighterVersionAsync(int id, [FromBody] UpdateFighterVersionDTO updateFighter)
        {
            ServiceResult<FighterVersion> result = await _fighterVersionService.UpdateAsync(id, updateFighter);

            return StatusCode(result.Status, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFighterVersionAsync(int id)
        {
            ServiceResult<FighterVersion> result = await _fighterVersionService.DeleteAsync(id);

            return StatusCode(result.Status, result);
        }
    }
}
