using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs
{
    public class BasketItemDTO
    {
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }

        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage ="Price Must BE Greater  Than Zero")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Must BE one item at least")]
        public int Quantity { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }


    }
}
