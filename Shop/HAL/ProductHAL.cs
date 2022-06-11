using Shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.API.HAL
{
    public static class ProductHAL
    {
        public static dynamic PaginateAsDynamic(string baseUrl, int index, int count, int total)
        {
            dynamic links = new ExpandoObject();
            links.self = new { href = "/api/products" };
            if (index < total)
            {
                links.next = new { href = $"/api/products?index={index + count}" };
                links.final = new { href = $"{baseUrl}?index={total - (total % count)}&count={count}" };
            }
            if (index > 0)
            {
                links.prev = new { href = $"/api/products?index={index - count}" };
                links.first = new { href = $"/api/products?index=0" };
            }
            return links;
        }

        public static Dictionary<string, object> PaginateAsDictionary(string baseUrl, int index, int count, int total)
        {
            var links = new Dictionary<string, object>();
            links.Add("self", new { href = "/api/products" });
            if (index < total)
            {
                links["next"] = new { href = $"/api/products?index={index + count}" };
                links["final"] = new { href = $"{baseUrl}?index={total - (total % count)}&count={count}" };
            }
            if (index > 0)
            {
                links["prev"] = new { href = $"/api/products?index={index - count}" };
                links["first"] = new { href = $"/api/products?index=0" };
            }
            return links;
        }

        public static dynamic ToResource(this Product product)
        {
            var resource = product.ToDynamic();
            resource._links = new
            {
                self = new
                {
                    href = $"/api/products/{product.Id}"
                },
                sales = new
                {
                    href = $"/api/sales/getsalesbyproductid/{product.Id}"
                }
            };
            return resource;
        }

        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> result = new ExpandoObject();
            var properties = TypeDescriptor.GetProperties(value.GetType());
            foreach (PropertyDescriptor prop in properties)
            {
                if (Ignore(prop)) continue;
                result.Add(prop.Name, prop.GetValue(value));
            }
            return result;
        }

        private static bool Ignore(PropertyDescriptor prop)
        {
            return prop.Attributes.OfType<System.Text.Json.Serialization.JsonIgnoreAttribute>().Any();
        }
    }
}
