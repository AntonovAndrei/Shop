using GraphQL.Types;
using Shop.API.GraphQL.Mutations;
using Shop.API.GraphQL.Queries;
using Shop.Data.IRepositories;

namespace Shop.API.GraphQL.Schemas
{
    public class AutoSchema : Schema
    {
        public AutoSchema(IProductRepository db)
        {
            Query = new ProductQuery(db);
            Mutation = new ProductMutation(db);
        }
    }
}
