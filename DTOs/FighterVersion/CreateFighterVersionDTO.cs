using System.ComponentModel.DataAnnotations;

namespace SF_API.DTOs.FighterVersion
{
    public class CreateFighterVersionDTO
    {
        [Required(ErrorMessage = "El campo IdFighter es obligatorio")]
        public int IdFighter { get; set; }

        [Required(ErrorMessage = "El campo IdGame es obligatorio")]
        public int IdGame { get; set; }

        [Required(ErrorMessage = "El campo nombre de la version es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo nombre de la version no puede tener mas de 50 caracteres ")]
        public string VersionName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo descripcion es obligatorio")]
        [MaxLength]
        public string Description { get; set; } = string.Empty;
    }
}
