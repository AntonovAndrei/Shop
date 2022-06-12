using GraphQL;
using GraphQL.Types;
using Shop.API.GraphQL.GraphTypes;
using Shop.Core.Entities;
using Shop.Data.IRepositories;

namespace Shop.API.GraphQL.Queries
{
    public class ProductQuery : ObjectGraphType
    {
        private readonly IProductRepository _productRepository;

        public ProductQuery(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            Field<ListGraphType<ProductGraphType>>("Products", "Query to retrieve all products",
                resolve: GetAllProducts);

            Field<ProductGraphType>("Product", "Query to retrieve a specific product",
            new QueryArguments(MakeNonNullStringArgument("id", "The identifier of the product")),
            resolve: GetProduct);
        }

        private QueryArgument MakeNonNullStringArgument(string name, string description)
        {
            return new QueryArgument<NonNullGraphType<StringGraphType>>
            {
                Name = name,
                Description = description
            };
        }

        private Product GetProduct(IResolveFieldContext<object> context)
        {
            var id = context.GetArgument<int>("id");
            return _productRepository.GetById(id);
        }

        private IEnumerable<Product> GetAllProducts(IResolveFieldContext<object> context) => _productRepository.GetAll();
    }
}
