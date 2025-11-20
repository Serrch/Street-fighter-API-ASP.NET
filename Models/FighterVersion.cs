using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SF_API.Models
{
    public class FighterVersion
    {
        [Key]
        public int IdFighterVersion { get; set; }

        [ForeignKey("Fighter")]
        public int IdFighter { get; set; }

        [ForeignKey("Game")]
        public int IdGame { get; set; }

        [Required(ErrorMessage = "El campo nombre de la version es obligatorio")]
        [StringLength(50, ErrorMessage ="El campo nombre de la version no puede tener mas de 50 caracteres ")]
        public string VersionName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo descripcion es obligatorio")]
        [MaxLength]
        public string Description { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        public virtual ICollection<FighterMove> FighterMoves { get; set; } = new List<FighterMove>();

        [JsonIgnore]
        [InverseProperty("FighterVersions")]

        public virtual Fighter? Fighter { get; set; }

        [JsonIgnore]
        [InverseProperty("FighterVersions")]
        public virtual Game? Game { get; set; }
    }
}
