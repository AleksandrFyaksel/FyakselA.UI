using FyakselA.UI.Data;
using FyakselA.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов в контейнер.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Настройка контекста базы данных с использованием строки подключения
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Регистрация сервисов Identity с настройками пароля
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;
    options.Password.RequiredUniqueChars = 0;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Добавление Razor Pages
builder.Services.AddRazorPages();

// Добавление фильтра для разработчиков базы данных
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Регистрация ISuperService
builder.Services.AddScoped<ISuperService, SuperService>();

// Регистрация EmailSender для отправки электронных писем
builder.Services.AddTransient<IEmailSender, EmailSender>();

// Регистрация FakeEmailSender для имитации отправки электронных писем (для тестирования)
builder.Services.AddTransient<IEmailSender, FakeEmailSender>();

// Регистрация контроллеров с представлениями
builder.Services.AddControllersWithViews();

// Регистрация сервисов для работы с книгами и категориями
builder.Services.AddScoped<ICategoryService, MemoryCategoryService>();
builder.Services.AddScoped<IBookService, MemoryBookService>();

// Добавление поддержки сессий
builder.Services.AddSession();

var app = builder.Build();

// Настройка конвейера обработки HTTP-запросов.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint(); 
}
else
{
    app.UseExceptionHandler("/Home/Error"); 
    app.UseHsts(); // Включение HSTS
}

app.UseHttpsRedirection(); // Перенаправление на HTTPS
app.UseStaticFiles(); // Обслуживание статических файлов
app.UseRouting(); // Включение маршрутизации

app.UseSession(); // Использование сессий

app.UseAuthentication(); // Аутентификация
app.UseAuthorization(); // Авторизация

// Настройка маршрутов
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Поддержка Razor Pages

app.Run(); // Запуск приложения