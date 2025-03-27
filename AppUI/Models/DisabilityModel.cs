using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppUI.Models
{
    public class DisabilityModel
    {
        public int DisabilityId { get; set; }

        [Required(ErrorMessage = "Introduce el nombre de la discapacidad.")]
        [DisplayName("Nombre")]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [DisplayName("Descripción")]
        [MaxLength(255)]
        public string? Description { get; set; }

        [ValidateNever]
        public string IsActive { get; set; } = null!;
    }
}
