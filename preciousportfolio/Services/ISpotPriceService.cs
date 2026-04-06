using preciousportfolio.Models;

namespace preciousportfolio.Services
{
    public interface ISpotPriceService
    {
        Task<SpotPricesViewModel> GetSpotPricesAsync();
    }
}