using GraphQL;
using GraphQL.Types;
using Shop.API.GraphQL.GraphTypes;
using Shop.Core.Entities;
using Shop.Data.IRepositories;

namespace Shop.API.GraphQL.Mutations
{
    public class ProductMutation : ObjectGraphType
    {
        private readonly IProductRepository _productRepository;

        public ProductMutation(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            Field<ProductGraphType>(
                "createProduct",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "price" }
                ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    var price = context.GetArgument<int>("price");
                    var id = context.GetArgument<int>("id");

                    var product = new Product
                    {
                        Id = id,
                        Name = name,
                        Price = price
                    };
                    _productRepository.AddAsync(product);
                    return product;
                }
            );
        }
    }
}
