using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppUI.Models.User
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Introduce tu nombre de usuario o correo electrónico.")]
        [DisplayName("Usuario o correo electrónico")]
        [MaxLength(50)]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Introduce tu contraseña.")]
        [DisplayName("Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
