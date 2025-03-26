using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppUI.Models.User
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Selecciona el rol de usuario.")]
        [DisplayName("Rol de usuario")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Introduce el nombre de usuario.")]
        [DisplayName("Nombre de usuario")]
        [MaxLength(50)]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Introduce el correo electrónico.")]
        [DisplayName("Correo electrónico")]
        [MaxLength(100)]
        public string Email { get; set; } = null!;
    }
}
