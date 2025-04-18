using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppUI.Models
{
    public class ClassroomSubjectModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Selecciona el grupo de clases.")]
        [DisplayName("Grupo")]
        public int ClassroomId { get; set; }

        [Required(ErrorMessage = "Selecciona la materia.")]
        [DisplayName("Materia")]
        public int SubjectId { get; set; }

        [ValidateNever]
        [DisplayName("Materia")]
        public string SubjectName { get; set; } = null!;
    }
}
