using System.ComponentModel.DataAnnotations;

namespace GR30323.Blazor.Services
{
    public class Book
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "Название книги обязательно для заполнения.")]
        public string Title { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Описание книги обязательно для заполнения.")]
        public string Description { get; set; } = string.Empty; 

        public string? Image { get; set; } 
        public string Author { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Дата публикации обязательна.")]
        public int PublicationDate { get; set; } 

        [Required(ErrorMessage = "Цена обязательна.")]
        public double Price { get; set; } 

        
    }
}