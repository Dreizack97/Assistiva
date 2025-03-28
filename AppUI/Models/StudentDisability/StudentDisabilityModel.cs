using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppUI.Models.StudentDisability
{
    public class StudentDisabilityModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Estudiante requerido")]
        [DisplayName("Estudiante")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Selecciona el tipo de discapacidad.")]
        [DisplayName("Discapacidad")]
        public int DisabilityId { get; set; }
    }
}