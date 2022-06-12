using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.API.Models
{
    public class BuyerDto
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Buyer name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Buyer age")]
        public int Age { get; set; }
    }
}
