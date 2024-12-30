﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GR30323.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название книги обязательно для заполнения.")]
        [Display(Name = "Название книги")]
        public required string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Описание книги обязательно для заполнения.")]
        public required string Description { get; set; } = string.Empty;

        public string? Image { get; set; }
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Дата публикации обязательна.")]
        public int PublicationDate { get; set; }

        [Required(ErrorMessage = "Цена обязательна.")]
        public double Price { get; set; }

        // Навигационные поля
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }
        public string? ErrorMessage { get; set; }
        public int BookId { get; set; }
    }
}