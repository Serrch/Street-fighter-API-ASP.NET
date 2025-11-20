using System.ComponentModel.DataAnnotations;

namespace SF_API.DTOs.Fighter
{
    public class CreateFighterDTO
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo nombre no puede tener mas de 50 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo historia es obligatorio")]
        [MaxLength]
        public string History { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo descripcion es obligatorio")]
        [MaxLength]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo estilo de pelea es obligatorio")]
        [StringLength(100, ErrorMessage = "El campo estilo de pelea no puede tener mas de 100 caracteres")]
        public string FightStyle { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo arquetipo es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo arquetipo no puede tener mas de 50 caracteres")]
        public string Archetype { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo nacionalidad es obligatorio")]
        [StringLength(100, ErrorMessage = "El campo nacionalidad no puede tener mas de 100 caracteres")]
        public string Nationality { get; set; } = string.Empty;

    }
}
