using Microsoft.EntityFrameworkCore;
using GR30323.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Подключение к базе данных
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Настройка контекста базы данных
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

// Добавление сервисов в контейнер
builder.Services.AddControllers();

// Настройка CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Настройка HTTPS
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 7202; 
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
});

// Настройка Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "Описание API"
    });
});

var app = builder.Build();

// Настройка конвейера HTTP-запросов
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "swagger"; // Доступ к Swagger UI по /swagger
    });
}

else
{
    // В продакшене добавляем дополнительную защиту
    app.UseHsts(); // Добавляет заголовок Strict-Transport-Security
}

// Настройка конвейера в правильном порядке
app.UseHttpsRedirection(); // Перенаправление на HTTPS
app.UseStaticFiles(); // Обслуживание статических файлов
app.UseCors("AllowAll"); // Настройка CORS

app.UseRouting(); // Настройка маршрутизации

app.UseAuthorization(); // Настройка авторизации

app.MapControllers(); // Настройка маршрутов для контроллеров

// Инициализация данных (закомментировано в DbInitializer.cs)
// await DbInitializer.SeedData(app);

app.Run(); // Запуск приложения