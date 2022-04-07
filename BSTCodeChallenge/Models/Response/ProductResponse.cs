using System.ComponentModel.DataAnnotations;
using static BSTCodeChallenge.Models.DataValidation.DataValidation;

namespace BSTCodeChallenge.Models
{
    public class ProductResponse
    {
        [Required]
        [NotEmpty]
        public Guid Id { get; set; }
        [Required]
        [NotEmpty]
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public double Cost { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryPopularity { get; set; }
        public string CreationDate { get; set; }
        public DetailsResponse Details { get; set; }
    }
}
