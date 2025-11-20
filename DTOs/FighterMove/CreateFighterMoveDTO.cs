using System.ComponentModel.DataAnnotations;

namespace SF_API.DTOs.FighterMove
{
    public class CreateFighterMoveDTO
    {
        [Required(ErrorMessage = "El campo IdFighterVersion es obligatorio")]
        public int IdFighterVersion { get; set; }

        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El campo nombre no puede tener mas de 100 caracteres ")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo descripcion es obligatorio")]
        [MaxLength]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo super movimiento es obligatorio")]
        public bool IsSuperMove { get; set; }
    }
}
