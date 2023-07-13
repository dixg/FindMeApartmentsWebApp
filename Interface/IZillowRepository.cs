using System;
using FindMeApartmentsWebApp.Models;

namespace FindMeApartmentsWebApp.Interfaces
{
    public interface IZillowRepository
    {
        IAsyncEnumerable<ApartmentProperty> GetAll();
        //IEnumerable<ApartmentProperty> GetAll();
        Task<IEnumerable<ApartmentProperty>> GetZipcode(string addressZipcode);
        Task<IEnumerable<ApartmentProperty>> GetBedrooms(double bedrooms);
        // Task<ApartmentProperty> AddToFavorites(string id);
        // Task<ApartmentProperty> RemoveFromFavorites(string id);
    }
}


// The interface is useful here as it provides a contract between the controller and the model.
// The interface defines the methods that the model must implement, and the controller can work with any class that implements the interface, without having to know the details of the underlying implementation. 
// This allows for greater decoupling between the controller and the model and makes the application more maintainable and easier to test.

