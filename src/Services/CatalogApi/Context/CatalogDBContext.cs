using MongoRepo.Context;

namespace CatalogApi.Context
{
    public class CatalogDBContext : ApplicationDbContext  
    {
        static IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
        static string connectionString = configuration.GetConnectionString("CatalogAPI");
        static string databaseName = configuration.GetValue<string>("DatabaseName");
        public CatalogDBContext() : base(connectionString, databaseName)
        {


        }
    }
}
