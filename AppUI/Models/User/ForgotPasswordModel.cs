using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AppUI.Models.User
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Introduce tu nombre de usuario o correo electrónico.")]
        [DisplayName("Usuario o correo electrónico")]
        public string Email { get; set; } = null!;
    }
}
