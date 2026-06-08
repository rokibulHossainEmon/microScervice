using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CatalogApi.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] // Tells MongoDB to treat this string as an ObjectId
        public string Id { get; set; } // Changed from int to string

        public string Name { get; set; }

        public string Summary { get; set; } // Added missing property

        public string Description { get; set; }

        public string Category { get; set; }

        public string ImageFile { get; set; }

        public decimal Price { get; set; }
    }
}