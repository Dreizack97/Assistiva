using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppUI.Models
{
    public class SubjectModel
    {
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Introduce el código de la materia.")]
        [DisplayName("Código")]
        [MaxLength(10)]
        public string Code { get; set; } = null!;

        [Required(ErrorMessage = "Introduce el nombre de la materia.")]
        [DisplayName("Nombre")]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [DisplayName("Descripción")]
        [MaxLength(255)]
        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }
}
