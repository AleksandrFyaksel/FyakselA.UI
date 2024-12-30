namespace GR30323.API.Controllers
{
    internal interface IBookingService
    {
        Task<ResultType> GetProductByIdAsync(int id);
    }
}