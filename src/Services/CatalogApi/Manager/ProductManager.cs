using CatalogApi.Interfaces.Manager;
using CatalogApi.Models;
using CatalogApi.Repository;
using MongoRepo.Manager;
using MongoRepo.Repository;

namespace CatalogApi.Manager
{
    public class ProductManager : CommonManager<Product>,IProductManager
    {
        public ProductManager() : base(new ProductRepository())
        {

        }
    }
}
