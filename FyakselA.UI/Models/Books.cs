using System.ComponentModel.DataAnnotations;

namespace GR30323.Domain.Entities
{
    public class Books
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "Название книги обязательно для заполнения.")]
        [Display(Name = "Название книги")]
        public required string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Описание книги обязательно для заполнения.")]
        public required string Description { get; set; } = string.Empty; 

        public string? Image { get; set; } 
        public required string Author { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Дата публикации обязательна.")]
        public int PublicationDate { get; set; } 

        [Required(ErrorMessage = "Цена обязательна.")]
        public double Price { get; set; } 

        public int CategoryId { get; set; } 
        public Category? Category { get; set; } 
    }
}