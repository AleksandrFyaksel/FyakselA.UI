namespace GR30323.Blazor.Services
{
    public interface IBookService<T> where T : class
    {
        
        event Action ListChanged;

        
        IEnumerable<T> Books { get; }

        
        int CurrentPage { get; }

       
        int TotalPages { get; }

        
        Task GetBooks(int pageNo = 1, int pageSize = 3);
    }
}