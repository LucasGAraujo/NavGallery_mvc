using System.ComponentModel.DataAnnotations;

namespace NavGallery.Models
{
    public class Marca : ModelId
    {
        [Required(ErrorMessage = "O campo Name é obrigatório.")]
        public string Name { get; set; }
        
    }
}
