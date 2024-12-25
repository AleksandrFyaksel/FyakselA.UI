using System.ComponentModel.DataAnnotations;

namespace FyakselA.UI.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Название книги обязательно для заполнения.")]
        [Display(Name = "Название книги")]
        public string Name { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Автор книги обязателен для заполнения.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Автор должен содержать от 3 до 100 символов.")]
        public string Author { get; set; } = string.Empty; 
    }
}