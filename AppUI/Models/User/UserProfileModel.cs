using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppUI.Models.User
{
    public class UserProfile
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        [Required(ErrorMessage = "Introduce el nombre de usuario.")]
        [DisplayName("Nombre de usuario")]
        [MaxLength(50)]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Introduce tu correo electrónico")]
        [DisplayName("Correo electrónico")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        public string? UrlPicture { get; set; }

        [DisplayName("Último reestablecimiento de contraseña")]
        [DataType(DataType.DateTime)]
        public DateTime? LastPasswordReset { get; set; }

        [DisplayName("Último cambio de contraseña")]
        [DataType(DataType.DateTime)]
        public DateTime LastPasswordChange { get; set; }

        [DisplayName("Creación de cuenta")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Actualización de cuenta")]
        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }

        public StudentModel? StudentModel { get; set; }
    }
}