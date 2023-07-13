using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using FindMeApartmentsWebApp.Interfaces;
using FindMeApartmentsWebApp.Models;
using static System.Reflection.Metadata.BlobBuilder;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace FindMeApartmentsWebApp.Repos
{
    public class JsonZillowRepository : IZillowRepository
    {
        
        private List<ApartmentProperty> _apartmentList;
        private readonly List<ApartmentProperty> _favoriteProperties;

        public JsonZillowRepository()
        {
            string jsonData = System.IO.File.ReadAllText("ZillowData.json");

            // Deserialize JSON data into a ApartmentProperty object
            _apartmentList = JsonConvert.DeserializeObject<List<ApartmentProperty>>(jsonData);

            _favoriteProperties = new List<ApartmentProperty>();
        }

        public async IAsyncEnumerable<ApartmentProperty> GetAll()
        {
            await Task.Yield();

            foreach (ApartmentProperty apartment in _apartmentList)
            {
                yield return apartment;
            }
        }

        // GetAll can be implemented using IEnumerable
    //    public IEnumerable<ApartmentProperty> GetAll()
    //     {
    //         List<ApartmentProperty> apartments = new List<ApartmentProperty>();
    //         foreach (ApartmentProperty apartment in _apartmentList){
    //             apartments.Add(apartment);

    //         }
    //         return apartments.AsEnumerable();
    //     }

        public async Task<IEnumerable<ApartmentProperty>> GetZipcode(string addressZipcode)
        {
            await Task.Yield();

            return _apartmentList.Where(p => p.AddressZipcode == addressZipcode);
        }

        public async Task<IEnumerable<ApartmentProperty>> GetBedrooms(double bedrooms)
        {
            await Task.Yield();

            return _apartmentList.Where(p => p.Beds == bedrooms);
        }

        // public async Task<ApartmentProperty> AddToFavorites(string id)
        // {
        //     await Task.Yield();

        //     var property = _apartmentList.FirstOrDefault(p => p.Id == id);
        //     if (property == null)
        //     {
        //         return null;
        //     }

        //     List<ApartmentProperty> _favoriteProperties = new List<ApartmentProperty>();


        //     if (System.IO.File.Exists("Favorites.json"))
        //     {
        //         string json = System.IO.File.ReadAllText("Favorites.json");
        //         _favoriteProperties = JsonConvert.DeserializeObject<List<ApartmentProperty>>(json);
        //     }

        //     _favoriteProperties.Add(property);

        //     string updatedJson = JsonConvert.SerializeObject(_favoriteProperties);

        //     System.IO.File.WriteAllText("Favorites.json", updatedJson);

        //     return property;
        // }
        // public async Task<ApartmentProperty> RemoveFromFavorites(string id)
        // {
        //     string jsonData = await System.IO.File.ReadAllTextAsync("Favorites.json");
            
        //     var favoriteProperties = JsonConvert.DeserializeObject<List<ApartmentProperty>>(jsonData);

        //     var propertyToBeDeleted = favoriteProperties.FirstOrDefault(p => p.Id == id);
            
        //     if (propertyToBeDeleted != null)
        //     {
        //         favoriteProperties.Remove(propertyToBeDeleted);
        //         jsonData = JsonConvert.SerializeObject(favoriteProperties);
        //         await System.IO.File.WriteAllTextAsync("Favorites.json", jsonData);
        //         return propertyToBeDeleted;
        //     }
        //     else
        //     {
        //         return null;
        //     }
        // }
    }
}

