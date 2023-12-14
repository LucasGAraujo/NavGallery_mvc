using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NavGallery.Models
{
    public class Car : ModelId
    {
        public DateTime? DataInsert { get; set; } = DateTime.Now;
       
        [Required(ErrorMessage = "O campo Modelo é obrigatório.")]
        public string Modelo { get; set; }

        [Range(1900, 2100, ErrorMessage = "Por favor, insira um ano válido.")]
        public int AnoCar { get; set; }

        [Required(ErrorMessage = "O campo Sobre é obrigatório.")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "A descrição deve ter entre 10 e 100 caracteres.")]
        public string Sobre { get; set; }
        public string? FotoCar { get; set; }

        [NotMapped]
        public IFormFile FotoUpload { get; set; }

        [Required(ErrorMessage = "O campo MarcaId é obrigatório.")]
        public int MarcaId { get; set; }
        public Marca? Marca { get; set; }
    }
}
