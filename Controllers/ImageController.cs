using Microsoft.AspNetCore.Mvc;
using SF_API.Common;
using SF_API.DTOs.Image;
using SF_API.Interfaces;
using SF_API.Models;
using SF_API.Utils;

namespace SF_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImageById(int id)
        {
            ServiceResult<Image> result = await _imageService.GetByIdAsync(id);

            return StatusCode(result.Status, result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateImageAsync(CreateImageDTO createImage)
        {
            ServiceResult<Image> result = await _imageService.AddAsync(createImage);

            return StatusCode(result.Status, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImageAsync(int id, [FromBody] CreateImageDTO updateImage)
        {
            ServiceResult<Image> result = await _imageService.UpdateAsync(id, updateImage);

            return StatusCode(result.Status, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImageAsync(int id)
        {
            ServiceResult<bool> result = await _imageService.DeleteByIdAsync(id);
            return StatusCode(result.Status, result);
        }


    }
}
