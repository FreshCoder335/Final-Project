using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace preciousportfolio.Models
{
    public class Holding
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string MetalType { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string FormType { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.0001, 1000000)]
        public decimal WeightOz { get; set; }

        [Required]
        [StringLength(20)]
        public string Purity { get; set; } = string.Empty;

        [Required]
        [Range(1, 1000000)]
        public int Quantity { get; set; }

        [DataType(DataType.Date)]
        public DateTime? AcquiredDate { get; set; }

        [Range(0, 100000000)]
        public decimal? AcquiredPrice { get; set; }

        [StringLength(100)]
        public string? Dealer { get; set; }

        [StringLength(100)]
        public string? StorageLocation { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser? User { get; set; }
    }
}