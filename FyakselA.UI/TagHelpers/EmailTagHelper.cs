using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FyakselA.UI.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        private string domain = "@mail.ru"; 

        public string MailTo { get; set; } = string.Empty; 

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a"; 
            var address = MailTo + domain; 
            output.Attributes.Add("href", "mailto:" + address); 
            output.Content.SetContent(address); 
        }
    }
}