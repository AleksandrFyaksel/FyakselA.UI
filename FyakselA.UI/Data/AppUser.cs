using Microsoft.AspNetCore.Identity;

namespace FyakselA.UI.Data
{
    public class AppUser : IdentityUser
    {
        public byte[]? Avatar { get; set; } 
        public string MimeType { get; set; } = string.Empty; 
    }
}