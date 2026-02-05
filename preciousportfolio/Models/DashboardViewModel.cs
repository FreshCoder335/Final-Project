namespace preciousportfolio.Models
{
    public class DashboardViewModel
    {
        public string DisplayName { get; set; } = "User";
        public decimal TotalValueUsd { get; set; }
        public decimal GoldOz { get; set; }
        public decimal SilverOz { get; set; }
        public DateTime LastUpdated { get; set; }

        public List<HoldingsRow> Holdings { get; set; } = new();
        public List<ActivityItem> RecentActivity { get; set; } = new();

        public static DashboardViewModel Sample()
        {
            return new DashboardViewModel
            {
                DisplayName = "Jeremiah",
                TotalValueUsd = 0m,
                GoldOz = 0m,
                SilverOz = 0m,
                LastUpdated = DateTime.UtcNow,
                Holdings = new List<HoldingsRow>
                {
                    new HoldingsRow { Metal = "Gold", Ounces = 0m, AvgCostUsd = 0m, CurrentPriceUsd = 0m },
                    new HoldingsRow { Metal = "Silver", Ounces = 0m, AvgCostUsd = 0m, CurrentPriceUsd = 0m },
                },
                RecentActivity = new List<ActivityItem>
                {
                    new ActivityItem { Timestamp = DateTime.UtcNow.AddDays(-2), Message = "Created profile" },
                    new ActivityItem { Timestamp = DateTime.UtcNow.AddDays(-1), Message = "Enabled 2FA" },
                }
            };
        }
    }

    public class HoldingsRow
    {
        public string Metal { get; set; } = "";
        public decimal Ounces { get; set; }
        public decimal AvgCostUsd { get; set; }
        public decimal CurrentPriceUsd { get; set; }
    }

    public class ActivityItem
    {
        public DateTime Timestamp { get; set; }
        public string Message { get; set; } = "";
    }
}
