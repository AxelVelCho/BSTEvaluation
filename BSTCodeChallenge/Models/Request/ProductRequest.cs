using BSTCodeChallenge.Models.Request;
using System.ComponentModel.DataAnnotations;
using static BSTCodeChallenge.Models.DataValidation.DataValidation;

namespace BSTCodeChallenge.Models
{
    public class ProductRequest
    {
        [Required]
        [NotEmpty]
        public Guid Id { get; set; }
        [Required]
        [NotEmpty]
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public string CreationDate { get; set; }
        public DetailsRequest Details { get; set; }
    }
}
