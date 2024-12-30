namespace FyakselA.UI.Models
{
 
        public class ResponseData<T>
        {
            public required List<T> Items { get; set; } = new List<T>();
            public bool Success { get; set; }
            public string? ErrorMessage { get; set; }
        }
}
