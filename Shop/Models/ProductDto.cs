using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shop.API.Models
{
    public class ProductDto
    {

        public int Id { get; set; }
        [Required]
        [DisplayName("Product name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Product price")]
        public int Price { get; set; }
    }
}
