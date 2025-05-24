using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppUI.Models
{
    public class StudentModel
    {
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Introduce el nombre.")]
        [DisplayName("Nombre(s)")]
        [MaxLength(100)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Introduce el apellido paterno.")]
        [DisplayName("Apellido paterno")]
        [MaxLength(50)]
        public string PaternalLastName { get; set; } = null!;

        [DisplayName("Apellido materno")]
        [MaxLength(50)]
        public string? MaternalLastName { get; set; }

        [Required(ErrorMessage = "Selecciona el género.")]
        [DisplayName("Género")]
        public string Gender { get; set; } = null!;

        [Required(ErrorMessage = "Introduce la fecha de nacimiento.")]
        [DisplayName("Fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; } = new DateOnly(2000, 1, 1);

        [Required(ErrorMessage = "Intoduce el correo electrónico.")]
        [DisplayName("Correo electrónico")]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; } = null!;

        [Required(ErrorMessage = "Selecciona el nivel educativo.")]
        [DisplayName("Nivel educativo")]
        public string EducationLevel { get; set; } = null!;

        [DisplayName("Profesión")]
        [MaxLength(50)]
        public string? Profession { get; set; }

        [Required(ErrorMessage = "Selecciona el estatus del nivel educativo.")]
        [DisplayName("Estatus")]
        public string ProfessionStatus { get; set; } = null!;

        [Required(ErrorMessage = "Selecciona el estado civil.")]
        [DisplayName("Estado civil")]
        public string MaritalStatus { get; set; } = null!;

        [Required(ErrorMessage = "Selecciona el tipo de sangre.")]
        [DisplayName("Tipo de sangre")]
        public string BloodType { get; set; } = null!;

        [Required(ErrorMessage = "Introduce la calle.")]
        [DisplayName("Calle")]
        [MaxLength(75)]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "Introduce el número de casa.")]
        [DisplayName("Número")]
        [MaxLength(10)]
        public string Number { get; set; } = null!;

        [Required(ErrorMessage = "Introduce la colonia.")]
        [DisplayName("Colonia")]
        [MaxLength(50)]
        public string Neighborhood { get; set; } = null!;

        [Required(ErrorMessage = "Introduce la ciudad.")]
        [DisplayName("Ciudad")]
        [MaxLength(30)]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Introduce la código postal.")]
        [DisplayName("Código postal")]
        [Range(0, 99999)]
        public int PostalCode { get; set; }

        [Required(ErrorMessage = "Introduce la estado.")]
        [DisplayName("Estado")]
        [MaxLength(30)]
        public string State { get; set; } = null!;

        [Required(ErrorMessage = "Introduce la país.")]
        [DisplayName("País")]
        [MaxLength(75)]
        public string Country { get; set; } = null!;

        public string? PhotoUrl { get; set; }

        [ValidateNever]
        public string IsActive { get; set; } = null!;

        [ValidateNever]
        public string FullName => string.Join(" ", [FirstName, PaternalLastName, MaternalLastName]);
    }
}
