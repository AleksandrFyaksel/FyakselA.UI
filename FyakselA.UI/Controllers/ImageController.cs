﻿using FyakselA.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fyaksel.UI.Controllers
{

    public class ImageController(UserManager<AppUser> userManager) : Controller
    {
        public async Task<IActionResult> GetAvatar()
        {

            var email = User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await userManager.FindByEmailAsync(email);
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
