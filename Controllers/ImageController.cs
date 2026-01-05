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

            if(!result.Success) return NotFound(RespuestaFactory.Fail(result.Message, 404)); 

            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }

        [HttpPost]
        public async Task<IActionResult> CreateImageAsync(CreateImageDTO createImage)
        {
            ServiceResult<Image> result = await _imageService.AddAsync(createImage);

            if (!result.Success) return BadRequest(RespuestaFactory.Fail(result.Message, 400));

            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImageAsync(int id, [FromBody] CreateImageDTO updateImage)
        {
            ServiceResult<Image> result = await _imageService.UpdateAsync(id, updateImage);

            if (!result.Success)
            {
                if (result.ErrorType == ErrorType.NotFound) return NotFound(RespuestaFactory.Fail(result.Message, 404));

                return BadRequest(RespuestaFactory.Fail(result.Message, 400));

            }

            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImageAsync(int id)
        {
            ServiceResult<bool> result = await _imageService.DeleteByIdAsync(id);
            if (!result.Success)
            {
                if (result.ErrorType == ErrorType.NotFound) return NotFound(RespuestaFactory.Fail(result.Message, 404));
                return BadRequest(RespuestaFactory.Fail(result.Message, 400));
            }
            return Ok(RespuestaFactory.Ok(result.Message, result.Data));
        }


    }
}
