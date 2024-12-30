using GR30323.API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class DishesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public DishesController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    [HttpPost("{id}/image")]
    public async Task<IActionResult> SaveImage(int id, IFormFile image)
    {
        // Найти объект по Id
        var dish = await _context.FindAsync(id);
        if (dish == null)
        {
            return NotFound();
        }

        // Путь к папке wwwroot/Images
        var imagesPath = Path.Combine(_env.WebRootPath, "Images");
        // Получить случайное имя файла
        var randomName = Path.GetRandomFileName();
        // Получить расширение в исходном файле
        var extension = Path.GetExtension(image.FileName);
        // Задать в новом имени расширение как в исходном файле
        var fileName = Path.ChangeExtension(randomName, extension);
        // Полный путь к файлу
        var filePath = Path.Combine(imagesPath, fileName);

        // Создать файл и открыть поток для записи
        using var stream = System.IO.File.OpenWrite(filePath);
        // Скопировать файл в поток
        await image.CopyToAsync(stream);

        // Получить Url хоста
        var host = $"{Request.Scheme}://{Request.Host}";
        // Url файла изображения
        var url = $"{host}/Images/{fileName}";

        // Сохранить url файла в объекте
        dish.Image = url;
        await _context.SaveChangesAsync();

        return Ok();
    }
}