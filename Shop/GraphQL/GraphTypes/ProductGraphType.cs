using GraphQL.Types;
using Shop.Core.Entities;

namespace Shop.API.GraphQL.GraphTypes
{
    public class ProductGraphType : ObjectGraphType<Product>
    {
        public ProductGraphType()
        {
            Name = "product";
            Field(p => p.Id).Description("The product identifier");
            Field(p => p.Name).Description("The product name");
            Field(p => p.Price).Description("The product price");
        }
    }
}
