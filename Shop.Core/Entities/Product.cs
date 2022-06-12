using System.Text.Json.Serialization;

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
