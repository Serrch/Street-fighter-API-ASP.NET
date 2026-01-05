using System.ComponentModel.DataAnnotations;
using SF_API.Models;

namespace SF_API.DTOs.Image
{
    public class CreateImageDTO
    {

        [Required(ErrorMessage = "El campo id de la entidad es obligatorio")]
        public int EntityId { get; set; }

        [Required(ErrorMessage = "El campo tipo de entidad es obligatorio")]
        public EntityType EntityType { get; set; }

        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public ImageType ImageType { get; set; } 

        [Required(ErrorMessage ="El campo imagen es obligatorio")]
        public IFormFile Image {  get; set; }
    }
}
