using FyakselA.UI.Data;
using FyakselA.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ���������� �������� � ���������.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// ��������� ��������� ���� ������ � �������������� ������ �����������
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// ����������� �������� Identity � ����������� ������
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

// ���������� Razor Pages
builder.Services.AddRazorPages();

// ���������� ������� ��� ������������� ���� ������
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ����������� ISuperService
builder.Services.AddScoped<ISuperService, SuperService>();

// ����������� EmailSender ��� �������� ����������� �����
builder.Services.AddTransient<IEmailSender, EmailSender>();

// ����������� FakeEmailSender ��� �������� �������� ����������� ����� (��� ������������)
builder.Services.AddTransient<IEmailSender, FakeEmailSender>();

// ����������� ������������ � ���������������
builder.Services.AddControllersWithViews();

// ����������� �������� ��� ������ � ������� � �����������
builder.Services.AddScoped<ICategoryService, MemoryCategoryService>();
builder.Services.AddScoped<IBookService, MemoryBookService>();

// ���������� ��������� ������
builder.Services.AddSession();

var app = builder.Build();

// ��������� ��������� ��������� HTTP-��������.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint(); 
}
else
{
    app.UseExceptionHandler("/Home/Error"); 
    app.UseHsts(); // ��������� HSTS
}

app.UseHttpsRedirection(); // ��������������� �� HTTPS
app.UseStaticFiles(); // ������������ ����������� ������
app.UseRouting(); // ��������� �������������

app.UseSession(); // ������������� ������

app.UseAuthentication(); // ��������������
app.UseAuthorization(); // �����������

// ��������� ���������
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // ��������� Razor Pages

app.Run(); // ������ ����������