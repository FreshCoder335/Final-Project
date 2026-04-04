using System;
using System.Collections.Generic;

namespace preciousportfolio.Models
{
    // ViewModel for the Sales Report page
    public class SalesReportViewModel
    {
        // Filter values
        public string SelectedMetalType { get; set; } = "";
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Dropdown options for metal filter
        public List<string> MetalTypes { get; set; } = new();

        // Summary cards
        public int TotalSalesCount { get; set; }
        public decimal TotalProceeds { get; set; }
        public decimal TotalGainLoss { get; set; }

        // Table rows
        public List<SalesReportRow> Rows { get; set; } = new();
    }

    // One row in the Sales Report table
    public class SalesReportRow
    {
        public int Id { get; set; }

        public DateTime DateSold { get; set; }

        public string MetalType { get; set; } = "";
        public string Description { get; set; } = "";

        public int Quantity { get; set; }
        public decimal WeightOz { get; set; }

        public decimal Proceeds { get; set; }
        public decimal CostBasis { get; set; }
        public decimal GainLoss { get; set; }
    }
}