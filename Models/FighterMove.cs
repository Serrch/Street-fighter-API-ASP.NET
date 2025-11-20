using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SF_API.Models
{
    public class FighterMove
    {
        [Key]
        public int IdFighterMove { get; set; }

        [ForeignKey("FighterVersion")]
        public int IdFighterVersion { get; set; }

        [Required(ErrorMessage ="El campo nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El campo nombre no puede tener mas de 100 caracteres ")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo descripcion es obligatorio")]
        [MaxLength]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage ="El campo super movimiento es obligatorio")]
        public bool IsSuperMove { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        [JsonIgnore]
        public virtual FighterVersion? FighterVersion { get; set; }
    }
}
