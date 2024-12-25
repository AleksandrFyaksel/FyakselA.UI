using Microsoft.EntityFrameworkCore;
using GR30323.API.Data;

var builder = WebApplication.CreateBuilder(args);

// ����������� � ���� ������
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// ��������� ��������� ���� ������
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

// ���������� �������� � ���������
builder.Services.AddControllers();

// ��������� CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// ��������� HTTPS
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 7202; 
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
});

// ��������� Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "�������� API"
    });
});

var app = builder.Build();

// ��������� ��������� HTTP-��������
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "swagger"; // ������ � Swagger UI �� /swagger
    });
}

else
{
    // � ���������� ��������� �������������� ������
    app.UseHsts(); // ��������� ��������� Strict-Transport-Security
}

// ��������� ��������� � ���������� �������
app.UseHttpsRedirection(); // ��������������� �� HTTPS
app.UseStaticFiles(); // ������������ ����������� ������
app.UseCors("AllowAll"); // ��������� CORS

app.UseRouting(); // ��������� �������������

app.UseAuthorization(); // ��������� �����������

app.MapControllers(); // ��������� ��������� ��� ������������

// ������������� ������ (���������������� � DbInitializer.cs)
// await DbInitializer.SeedData(app);

app.Run(); // ������ ����������