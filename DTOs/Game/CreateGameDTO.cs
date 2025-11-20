using System.ComponentModel.DataAnnotations;

namespace SF_API.DTOs.Game
{
    public class CreateGameDTO
    {
        [Required(ErrorMessage = "El campo titulo es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo titulo no puede tener mas de 50 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo año es obligatorio")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "El año debe tener exactamente 4 caracteres")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "El año debe contener exactamente 4 dígitos numéricos")]
        public string Year { get; set; } = string.Empty;
    }
}
