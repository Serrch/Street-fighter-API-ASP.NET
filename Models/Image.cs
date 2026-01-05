using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SF_API.Models
{
    public enum EntityType
    {
        Fighter,
        Game,
        FighterVersion,
        FighterMove
    }

    public enum ImageType
    {
        thumbnail,
        portrait,
        full,
        sprite,
        icon,
        command,
        execution,
        logo,
    }

    public class Image
    {
        [Key]
        public int IdImage { get; set; }

        [Required(ErrorMessage ="El campo id de la entidad es obligatorio")]
        public int EntityId { get; set; }

        [Required(ErrorMessage = "El campo tipo de entidad es obligatorio")]
        [StringLength(50, ErrorMessage = "El tipo de entidad no puede tener más de 50 caracteres")]
        public EntityType EntityType { get; set; }

        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El campo nombre no puede tener mas de 100 caracteres ")]
        public ImageType ImageType { get; set; }

        [Required(ErrorMessage = "El campo ruta de imagen es obligatorio")]
        [MaxLength]
        public string ImagePath { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        [NotMapped]
        public Fighter? Fighter { get; set; }

        [NotMapped]
        public Game? Game { get; set; }

        [NotMapped]
        public FighterVersion? FighterVersion { get; set; }

        [NotMapped]
        public FighterMove? FighterMove { get; set; }

    }

    public class ResponseImage
    {
        public bool response {  get; set; } = false;
        public string imagePath { get; set; } = string.Empty;
    }
}
