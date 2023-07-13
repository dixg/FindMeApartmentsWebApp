using System.Collections.Generic;
using System.Linq;
using FindMeApartmentsWebApp.Interfaces;
using FindMeApartmentsWebApp.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;


namespace FindMeApartmentsWebApp.Repos
{
    public class MongoZillowRepository : IZillowRepository
    {
        private readonly IMongoCollection<ApartmentProperty> _apartmentCollection;

        public MongoZillowRepository(IMongoClient mongoClient, IOptions<MongodbSettings> mongodbSettings )
        {

            var database = mongoClient.GetDatabase(mongodbSettings.Value.databaseName);
            _apartmentCollection = database.GetCollection<ApartmentProperty>(mongodbSettings.Value.collectionName);
        }

        public async IAsyncEnumerable<ApartmentProperty> GetAll()
        {
            var cursor = await _apartmentCollection.FindAsync(FilterDefinition<ApartmentProperty>.Empty);

            while (await cursor.MoveNextAsync())
            {
                foreach (var apartment in cursor.Current)
                {
                    yield return apartment;
                }
            }
        }

        public async Task<IEnumerable<ApartmentProperty>> GetZipcode(string addressZipcode)
        {
            var filter = Builders<ApartmentProperty>.Filter.Eq(p => p.AddressZipcode, addressZipcode);

            var result = await _apartmentCollection.FindAsync(filter);

            return await result.ToListAsync();
        }

        public async Task<IEnumerable<ApartmentProperty>> GetBedrooms(double bedrooms)
        {
            var filter = Builders<ApartmentProperty>.Filter.Eq(p => p.Beds, bedrooms);

            var result = await _apartmentCollection.FindAsync(filter);

            return await result.ToListAsync();
        }

        // public async Task<ApartmentProperty> AddToFavorites(string id)
        // {
        //     var filter = Builders<ApartmentProperty>.Filter.Eq(p => p.Id, id);

        //     //add to Favorites

        // } 

        // public async Task<ApartmentProperty> RemoveFromFavorites(string id)
        // {
        //     var filter = Builders<ApartmentProperty>.Filter.Eq(p => p.Id, id);

        //    //add to Favorites

        // }
    }
}
