﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shop.Core.Entities
{
    public class Sale
    {
        public Sale()
        {
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }
        public int TotalAmount { get; set; }
        public int BuyerId { get; set; }

        [JsonIgnore]
        public virtual Buyer Buyer { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
