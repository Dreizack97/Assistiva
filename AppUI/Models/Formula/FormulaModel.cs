using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppUI.Models.Formula
{
    public class FormulaModel
    {
        public int FormulaId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Introduce el nombre de la fórmula.")]
        [DisplayName("Nombre")]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Introduce el contenido de la fórmula.")]
        [DisplayName("Contenido")]
        public string Content { get; set; } = null!;

        [DisplayName("Descripción")]
        public string? Description { get; set; }
    }
}
