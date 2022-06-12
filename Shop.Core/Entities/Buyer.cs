using System.Text.Json.Serialization;

namespace Shop.Core.Entities
{
    public class Buyer
    {
        public Buyer()
        {
            Sales = new HashSet<Sale>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        [JsonIgnore]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
