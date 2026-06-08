using CatalogApi.Context;
using CatalogApi.Interfaces.Repository;
using CatalogApi.Models;
using MongoRepo.Context;
using MongoRepo.Repository;

namespace CatalogApi.Repository
{
    public class ProductRepository : CommonRepository<Product>,IProductRepository
    {
       

        public ProductRepository() : base(new CatalogDBContext())
        {

        }

      
    }
}
