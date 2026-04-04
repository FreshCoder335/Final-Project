using System;
using System.Collections.Generic;

namespace preciousportfolio.Models
{
    /// <summary>
    /// ViewModel for the Dashboard page (UI-focused, not a DB model).
    /// </summary>
    public class DashboardViewModel
    {
        // User display name
        public string DisplayName { get; set; } = "User";

        // Total portfolio value (USD)
        public decimal TotalValueUsd { get; set; }

        // Total number of saved holding entries
        public int TotalHoldingsCount { get; set; }

        // Total ounces by metal
        public decimal GoldOz { get; set; }
        public decimal SilverOz { get; set; }

        // Last time dashboard data was refreshed
        public DateTime LastUpdated { get; set; }

        // Grouped summary (by metal)
        public List<HoldingsRow> Holdings { get; set; } = new();

        // General activity feed (future use)
        public List<ActivityItem> RecentActivity { get; set; } = new();

        // Recent holdings for preview card (latest 3–5 items)
        public List<RecentHoldingItem> RecentHoldings { get; set; } = new();

        /// <summary>
        /// Sample data for initial UI testing (not used in production).
        /// </summary>
        public static DashboardViewModel Sample()
        {
            return new DashboardViewModel
            {
                DisplayName = "Jeremiah",
                TotalValueUsd = 0m,
                TotalHoldingsCount = 0,
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
                },

                // Example preview data
                RecentHoldings = new List<RecentHoldingItem>
                {
                    new RecentHoldingItem
                    {
                        Id = 1,
                        MetalType = "Gold",
                        FormType = "Coin",
                        Description = "American Eagle",
                        WeightOz = 1.0m,
                        Quantity = 2,
                        Purity = "0.999"
                    },
                    new RecentHoldingItem
                    {
                        Id = 2,
                        MetalType = "Silver",
                        FormType = "Bar",
                        Description = "Generic Silver Bar",
                        WeightOz = 10.0m,
                        Quantity = 1,
                        Purity = "0.999"
                    }
                }
            };
        }
    }

    /// <summary>
    /// Grouped holdings summary row (by metal).
    /// </summary>
    public class HoldingsRow
    {
        public string Metal { get; set; } = "";
        public decimal Ounces { get; set; }

        // Average purchase price
        public decimal AvgCostUsd { get; set; }

        // Placeholder for future live pricing
        public decimal CurrentPriceUsd { get; set; }
    }

    /// <summary>
    /// Simple activity feed item.
    /// </summary>
    public class ActivityItem
    {
        public DateTime Timestamp { get; set; }
        public string Message { get; set; } = "";
    }

    /// <summary>
    /// Lightweight model for recent holdings preview.
    /// </summary>
    public class RecentHoldingItem
    {
        public int Id { get; set; }

        public string MetalType { get; set; } = "";
        public string FormType { get; set; } = "";
        public string Description { get; set; } = "";

        // Weight per unit (troy oz)
        public decimal WeightOz { get; set; }

        // Number of units owned
        public int Quantity { get; set; }

        // Metal purity (e.g., 0.999)
        public string Purity { get; set; } = "";
    }
}