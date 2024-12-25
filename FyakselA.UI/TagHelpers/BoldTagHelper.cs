using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FyakselA.UI.TagHelpers
{
    [HtmlTargetElement(Attributes = "bold")] 
    public class BoldTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
          
            output.Attributes.RemoveAll("bold");

            output.PreElement.SetHtmlContent("<strong>");

            output.PostElement.SetHtmlContent("</strong>");
        }
    }
}