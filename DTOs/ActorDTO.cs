using PeliculasAPI_Udemy.Validations;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI_Udemy.DTOs
{
    public class ActorDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Foto { get; set; }
    }

    public class ActorCreacionDTO
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [PesorArchivoValidation(4)]
        [FileTypeValidation(FileTypeGroup.Imagen)]
        public IFormFile Foto { get; set; }
    }

    public class ActorPatchDTO
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
