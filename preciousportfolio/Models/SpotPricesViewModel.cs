namespace preciousportfolio.Models
{
    public class SpotPricesViewModel
    {
        public decimal GoldUsdPerOz { get; set; }
        public decimal SilverUsdPerOz { get; set; }
        public decimal PlatinumUsdPerOz { get; set; }
        public decimal PalladiumUsdPerOz { get; set; }

        public DateTime LastUpdatedUtc { get; set; }
        public bool IsAvailable { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
