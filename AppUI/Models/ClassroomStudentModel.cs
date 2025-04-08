using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppUI.Models
{
    public class ClassroomStudentModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Selecciona el grupo del estudiante.")]
        [DisplayName("Grupo")]
        public int ClassroomId { get; set; }

        [Required(ErrorMessage = "Introduce el nombre del estudiante.")]
        public int StudentId { get; set; }

        [ValidateNever]
        [DisplayName("Estudiante")]
        public string StudentName { get; set; } = null!;
    }
}