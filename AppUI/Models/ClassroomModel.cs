using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppUI.Models
{
    public class ClassroomModel
    {
        public int ClassroomId { get; set; }

        [Required(ErrorMessage = "Selecciona un maestro para el grupo.")]
        [DisplayName("Maestro")]
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "Introduce el nombre del grupo.")]
        [DisplayName("Nombre de grupo")]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}