using System.ComponentModel.DataAnnotations;
using static BSTCodeChallenge.Models.DataValidation.DataValidation;

namespace BSTCodeChallenge.Models.Request
{
    public class DetailsRequest
    {
        [Required]
        [NotEmpty]
        public Guid DetailId { get; set; }
        [Required]
        [NotEmpty]
        public Guid ProductId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double StockQuantity { get; set; }
    }
}
