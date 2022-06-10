using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shop.Core.Entities
{
    public class Product
    {
        public Product()
        {
            Sales = new HashSet<Sale>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        [JsonIgnore]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
