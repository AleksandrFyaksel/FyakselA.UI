using GR30323.Domain.Entities;
using GR30323.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GR30323.API.Data
{
    public class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            // URI проекта
            var uri = "https://localhost:7002/";

            // Получение контекста БД
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Выполнение миграций
            await context.Database.MigrateAsync();

            // Заполнение данными, если таблицы пустые
            if (!context.Categories.Any() && !context.Books.Any())
            {
                // Создание категорий
                var categories = new[]
                {
                    new Category { Name = "Научная литература", NormalizedName = "ScientificLiterature" },
                    new Category { Name = "Фантастика", NormalizedName = "Fiction" },
                    new Category { Name = "Саморазвитие", NormalizedName = "SelfDevelopment" },
                };

                // Добавление категорий в контекст
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();

                // Создание книг с новыми названиями
                var books = new List<Book>
                {
                    new() {
                        Title = "Основы научного маркетинга",
                        Author = "Александр Петров",
                        PublicationDate = 2021,
                        Description = "Научный подход к маркетингу",
                        Price = 18.99,
                        Image = uri + "Images/1.jpg",
                        Category = categories.First(c => c.NormalizedName == "ScientificLiterature")
                    },
                    new Book
                    {
                        Title = "Мир фантастических проектов",
                        Author = "Елена Смирнова",
                        PublicationDate = 2020,
                        Description = "Управление проектами в мире фантастики",
                        Price = 25.50,
                        Image = uri + "Images/2.jpg",
                        Category = categories.First(c => c.NormalizedName == "Fiction")
                    },
                    new Book
                    {
                        Title = "Управление финансами для всех",
                        Author = "Игорь Кузнецов",
                        PublicationDate = 2022,
                        Description = "Как эффективно управлять своими финансами",
                        Price = 29.99,
                        Image = uri + "Images/9.jpg",
                        Category = categories.First(c => c.NormalizedName == "SelfDevelopment")
                    },
                    new Book
                    {
                        Title = "Секреты успешной жизни",
                        Author = "Анна Иванова",
                        PublicationDate = 2023,
                        Description = "Как достичь успеха в жизни",
                        Price = 15.75,
                        Image = uri + "Images/4.jpg",
                        Category = categories.First(c => c.NormalizedName == "SelfDevelopment")
                    },
                };

                // Добавление книг в контекст
                await context.Books.AddRangeAsync(books);
                await context.SaveChangesAsync();
            }
        }
    }
}