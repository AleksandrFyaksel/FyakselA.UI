using GR30323.Blazor.Components;
using GR30323.Blazor.Services;
using GR30323.Domain.Entities;
using GR30323.Domain.Models;
using System.Net.Http.Json;

namespace GR30323.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Добавление сервисов в контейнер
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Регистрация ApiBookService как IBookService
            builder.Services.AddHttpClient<IBookService<Book>, ApiBookService>(c =>
                c.BaseAddress = new Uri("https://localhost:7002/api/books"));
            builder.Services.AddScoped<IBookService<Book>, ApiBookService>();

            var app = builder.Build();

            // Конфигурация конвейера HTTP-запросов
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAntiforgery();
            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}