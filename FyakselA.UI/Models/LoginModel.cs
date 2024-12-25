using System.ComponentModel.DataAnnotations;

namespace FyakselA.UI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email обязателен.")]
        [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты.")]
        public string Email { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Пароль обязателен.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty; 
    }
}