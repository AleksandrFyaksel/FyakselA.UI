using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using FyakselA.UI.Data;

namespace FyakselA.UI.Controllers
{
    public class UserController : Controller
    {
        //private readonly UserManager<AppUser> _userManager;

        //public UserController(UserManager<AppUser> userManager)
        //{
        //    _userManager = userManager;
        //}

        //[HttpPost]
        //public async Task<IActionResult> UploadAvatar(IFormFile avatar)
        //{
        //    if (avatar != null && avatar.Length > 0)
        //    {
        //        var extProvider = new FileExtensionContentTypeProvider();
        //        if (!extProvider.TryGetContentType(avatar.FileName, out var mimeType))
        //        {
        //            mimeType = "application/octet-stream";
        //        }

        //        using (var memoryStream = new MemoryStream())
        //        {
        //            await avatar.CopyToAsync(memoryStream);
        //            var user = await _userManager.GetUserAsync(User);
        //            user.Avatar = memoryStream.ToArray();
        //            user.MimeType = mimeType;

        //            await _userManager.UpdateAsync(user);
        //        }

        //        return RedirectToAction("Profile");
        //    }

        //    return BadRequest("Не удалось загрузить аватар.");
        //}

        //public async Task<IActionResult> GetAvatar()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user?.Avatar != null)
        //    {
        //        var extProvider = new FileExtensionContentTypeProvider();
        //        var mimeType = user.MimeType ?? "application/octet-stream";
        //        return File(user.Avatar, mimeType);
        //    }

        //    return NotFound();
        //}

        
        //public async Task<IActionResult> UploadAvatar()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    return View(user);
        //}



            private readonly UserManager<AppUser> _userManager;
            private readonly IWebHostEnvironment _hostingEnvironment; 

            public UserController(UserManager<AppUser> userManager, IWebHostEnvironment hostingEnvironment)
            {
                _userManager = userManager;
                _hostingEnvironment = hostingEnvironment;
            }

            public async Task<IActionResult> GetAvatar()
            {
                var user = await _userManager.GetUserAsync(User);
                if (user?.Avatar != null)
                {
                    var extProvider = new FileExtensionContentTypeProvider();
                    var mimeType = user.MimeType ?? "application/octet-stream";
                    return File(user.Avatar, mimeType);
                }
                else
                {
                    
                    var path = Path.Combine(_hostingEnvironment.WebRootPath, "images", "default-avatar.png"); 
                    if (System.IO.File.Exists(path))
                    {
                        return PhysicalFile(path, "image/png"); 
                    }
                }

                return NotFound(); 
            }

        }
    }

