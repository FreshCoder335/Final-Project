using System;
using System.ComponentModel.DataAnnotations;

namespace preciousportfolio.Models
{
    // ViewModel for selling part or all of a holding
    public class SellHoldingViewModel
    {
        public int HoldingId { get; set; }

        public string MetalType { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal WeightOz { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal? AcquiredPrice { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateSold { get; set; } = DateTime.Today;

        [Required]
        [Range(1, 1000000)]
        public int QuantitySold { get; set; }

        [Required]
        [Range(0, 1000000000)]
        public decimal Proceeds { get; set; }
    }
}
