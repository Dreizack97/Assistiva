using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppUI.Models.User
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Código de recuperación no valido.")]
        [MaxLength(16)]
        public string RecoveryCode { get; set; } = null!;

        [Required(ErrorMessage = "Introduce tu nueva contraseña.")]
        [DisplayName("Nueva contraseña")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos ocho caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).+$", ErrorMessage = "La contraseña no cumple con los requisitos: (al menos una letra mayúscula, al menos una letra minúscula, al menos un número y al menos un carácter especial).")]
        public string NewPassword { get; set; } = null!;

        [Required(ErrorMessage = "Confirma tu nueva contraseña.")]
        [DisplayName("Confirma tu contraseña")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos ocho caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).+$", ErrorMessage = "La contraseña no cumple con los requisitos: (al menos una letra mayúscula, al menos una letra minúscula, al menos un número y al menos un carácter especial).")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
