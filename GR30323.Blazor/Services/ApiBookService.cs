using GR30321.Blazor.Services;
using GR30323.Domain.Entities;
using GR30323.Domain.Models;
using System.Net.Http.Json;

namespace GR30323.Blazor.Services
{
    public class ApiBookService : IBookService<Book>
    {
        private readonly HttpClient _http;
        private List<Book> _books = new List<Book>(); 
        private int _currentPage = 1;
        private int _totalPages = 1;

        public IEnumerable<Book> Books => _books;
        public int CurrentPage => _currentPage;
        public int TotalPages => _totalPages;
        public event Action? ListChanged; 

        public ApiBookService(HttpClient http)
        {
            _http = http;
        }

        public async Task GetBooks(int pageNo, int pageSize)
        {
            // Url сервиса API
            var uri = _http.BaseAddress.AbsoluteUri;

            // данные для Query запроса
            var queryData = new Dictionary<string, string?>
            {
                { "pageNo", pageNo.ToString() },
                { "pageSize", pageSize.ToString() }
            };
            var query = QueryString.Create(queryData);

            // Отправить запрос http
            var result = await _http.GetAsync(uri + query.Value);

            // В случае успешного ответа
            if (result.IsSuccessStatusCode)
            {
                // получить данные из ответа
                var responseData = await result.Content.ReadFromJsonAsync<ResponseData<ListModel<Book>>>();

                // Проверка на null
                if (responseData == null)
                {
                    Console.WriteLine("responseData is null");
                    _books = new List<Book>(); 
                }
                else if (responseData.Data == null)
                {
                    Console.WriteLine("responseData.Data is null");
                    _books = new List<Book>(); 
                }
                else if (responseData.Data.Items == null)
                {
                    Console.WriteLine("responseData.Data.Items is null");
                    _books = new List<Book>(); 
                }
                else
                {
                    // обновить параметры
                    _currentPage = responseData.Data.CurrentPage;
                    _totalPages = responseData.Data.TotalPages;
                    _books = responseData.Data.Items; 
                    ListChanged?.Invoke(); 
                }
            }
            else
            {
                Console.WriteLine($"Ошибка при получении данных: {result.StatusCode}");
                _books = new List<Book>(); 
                _currentPage = 1;
                _totalPages = 1;
            }
        }
    }
}