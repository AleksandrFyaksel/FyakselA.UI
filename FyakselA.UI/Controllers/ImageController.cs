using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IO;
using System.Threading.Tasks;
using FyakselA.UI.Data;

namespace Fyaksel.UI.Controllers
{
    public class ImageController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ImageController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> GetAvatar()
        {
            var email = User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            if (user.Avatar != null && user.Avatar.Length > 0)
                return File(user.Avatar, user.MimeType);

            var imagePath = Path.Combine("images", "1.jpg");
            return File(imagePath, "image/jpg");
        }
    }
}