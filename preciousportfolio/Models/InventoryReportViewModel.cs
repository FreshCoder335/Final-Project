using System.Collections.Generic;

namespace preciousportfolio.Models
{
    // ViewModel for the Inventory Report page
    public class InventoryReportViewModel
    {
        // Total value based on acquisition prices
        public decimal TotalHoldingsValue { get; set; }

        // Total ounces across all holdings
        public decimal TotalWeightOz { get; set; }

        // Same as cost basis (no API for now)
        public decimal EstimatedValue { get; set; }

        // Rows displayed in the table
        public List<InventoryReportRow> Rows { get; set; } = new();
    }

    // Represents a single row in the report
    public class InventoryReportRow
    {
        public int Id { get; set; }

        public string MetalType { get; set; } = "";
        public string Description { get; set; } = "";

        public decimal WeightOz { get; set; }
        public string Purity { get; set; } = "";
        public int Quantity { get; set; }

        public string StorageLocation { get; set; } = "";

        // Calculated: Weight × Quantity
        public decimal TotalOunces { get; set; }

        // Optional price per unit
        public decimal? AcquiredPrice { get; set; }

        // Calculated: Price × Quantity
        public decimal CostBasis { get; set; }
    }
}
