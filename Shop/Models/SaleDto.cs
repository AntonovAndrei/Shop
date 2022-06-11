using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.API.Models
{
    public class SaleDto
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Sale date")]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }
        [Required]
        [DisplayName("Total purchase price")]
        public int TotalAmount { get; set; }
        [Required]
        [DisplayName("Id of the person who bought")]
        public int BuyerId { get; set; }
    }
}
