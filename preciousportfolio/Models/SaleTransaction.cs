using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace preciousportfolio.Models
{
    // Stores a completed sale transaction
    public class SaleTransaction
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string MetalType { get; set; } = "";

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = "";

        [Range(1, 1000000)]
        public int Quantity { get; set; }

        [Range(0.0001, 1000000)]
        public decimal WeightOz { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateSold { get; set; }

        // Total amount received from the sale
        [Range(0, 1000000000)]
        public decimal Proceeds { get; set; }

        // Original total cost basis for the sold items
        [Range(0, 1000000000)]
        public decimal CostBasis { get; set; }

        // Link this sale back to the original holding
        public int? HoldingId { get; set; }
        public Holding? Holding { get; set; }

        // Link sale record to the logged-in user
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }
    }
}