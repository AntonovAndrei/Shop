using Shop.Core.Entities;
using System.ComponentModel;
using System.Dynamic;

namespace Shop.API.HAL
{
    public static class SaleHAL
    {
        public static dynamic PaginateAsDynamic(string baseUrl, int index, int count, int total)
        {
            dynamic links = new ExpandoObject();
            links.self = new { href = "/api/sales" };
            if (index < total)
            {
                links.next = new { href = $"/api/sales?index={index + count}" };
                links.final = new { href = $"{baseUrl}?index={total - (total % count)}&count={count}" };
            }
            if (index > 0)
            {
                links.prev = new { href = $"/api/sales?index={index - count}" };
                links.first = new { href = $"/api/sales?index=0" };
            }
            return links;
        }

        public static Dictionary<string, object> PaginateAsDictionary(string baseUrl, int index, int count, int total)
        {
            var links = new Dictionary<string, object>();
            links.Add("self", new { href = "/api/sales" });
            if (index < total)
            {
                links["next"] = new { href = $"/api/sales?index={index + count}" };
                links["final"] = new { href = $"{baseUrl}?index={total - (total % count)}&count={count}" };
            }
            if (index > 0)
            {
                links["prev"] = new { href = $"/api/sales?index={index - count}" };
                links["first"] = new { href = $"/api/sales?index=0" };
            }
            return links;
        }

        public static dynamic ToResource(this Sale sale)
        {
            var resource = sale.ToDynamic();
            resource._links = new
            {
                self = new
                {
                    href = $"/api/sales/{sale.Id}"
                },
                buyer = new
                {
                    href = $"/api/buyers/{sale.BuyerId}"
                },
                products = new
                {
                    href = $"/api/products/getproductsbysaleid/{sale.Id}"
                }
            };
            return resource;
        }

        private static bool Ignore(PropertyDescriptor prop)
        {
            return prop.Attributes.OfType<System.Text.Json.Serialization.JsonIgnoreAttribute>().Any();
        }
    }
}
