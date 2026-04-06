using System.Net.Http;
using System.Text.Json;
using preciousportfolio.Models;

namespace preciousportfolio.Services
{
    public class GoldApiSpotPriceService : ISpotPriceService
    {
        private readonly HttpClient _httpClient;

        public GoldApiSpotPriceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.gold-api.com/");
        }

        public async Task<SpotPricesViewModel> GetSpotPricesAsync()
        {
            var result = new SpotPricesViewModel();

            try
            {
                result.GoldUsdPerOz = await GetPriceAsync("XAU");
                result.SilverUsdPerOz = await GetPriceAsync("XAG");
                result.PlatinumUsdPerOz = await GetPriceAsync("XPT");
                result.PalladiumUsdPerOz = await GetPriceAsync("XPD");

                result.LastUpdatedUtc = DateTime.UtcNow;
                result.IsAvailable = true;

                return result;
            }
            catch (HttpRequestException ex)
            {
                result.IsAvailable = false;
                result.ErrorMessage = $"HTTP error loading spot prices: {ex.Message}";
                return result;
            }
            catch (Exception ex)
            {
                result.IsAvailable = false;
                result.ErrorMessage = $"Unable to load spot prices: {ex.Message}";
                return result;
            }
        }

        private async Task<decimal> GetPriceAsync(string symbol)
        {
            using var response = await _httpClient.GetAsync($"price/{symbol}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            if (!doc.RootElement.TryGetProperty("price", out var priceElement))
            {
                throw new Exception($"Price was not returned for symbol {symbol}.");
            }

            return priceElement.GetDecimal();
        }
    }
}