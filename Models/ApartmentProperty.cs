using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FindMeApartmentsWebApp.Models
{
    public class ApartmentProperty
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        // public string Id { get; set; }
        public string? Price { get; set; }
        public int UnformattedPrice { get; set; }
        public string? Address { get; set; }
        public string? AddressStreet { get; set; }
        public string? AddressCity { get; set; }
        public string? AddressState { get; set; }
        public string? AddressZipcode { get; set; }
        public double? Beds { get; set; }
        public double? Baths { get; set; }
        public int? Area { get; set; }
    }
}





//Before mongodb
// namespace FindMeApartmentsWebApp.Models;
// public class ApartmentProperty
// {
//     public string? Id { get; set; }
//     public string? Price { get; set; }
//     public int UnformattedPrice { get; set; }
//     public string? Address { get; set; }
//     public string? AddressStreet { get; set; }
//     public string? AddressCity { get; set; }
//     public string? AddressState { get; set; }
//     public string? AddressZipcode { get; set; }
//     public double? Beds { get; set; }
//     public double? Baths { get; set; }
//     public int? Area { get; set; }

// }


