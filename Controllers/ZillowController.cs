using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using FindMeApartmentsWebApp.Interfaces;
using FindMeApartmentsWebApp.Models;


namespace FindMeApartmentsWebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ZillowController : ControllerBase
{
    private readonly IZillowRepository _zillowRepository;

    public ZillowController(IZillowRepository zillowRepository)
    {
        _zillowRepository = zillowRepository;
    }

    [HttpGet]
    public async IAsyncEnumerable<ApartmentProperty> GetAll()
    {
        await foreach (var apartment in _zillowRepository.GetAll())
        {
            yield return apartment;
        }
    }

    // [HttpGet]
    // public IEnumerable<ApartmentProperty> GetAll()
    // {
    //     return (IEnumerable<ApartmentProperty>)_zillowRepository.GetAll();
    // }


    [HttpGet("zipcode/{addressZipcode}")]
    public async Task<ActionResult<IEnumerable<ApartmentProperty>>> GetZipcode(string addressZipcode)
    {
        var apartments = await _zillowRepository.GetZipcode(addressZipcode);

        if (apartments == null)
        {
            return NotFound();
        }

        return Ok(apartments);
    }

    [HttpGet("bedrooms/{bedrooms}")]
    public async Task<ActionResult<IEnumerable<ApartmentProperty>>> GetBedrooms(double bedrooms)
    {
        var apartments = await _zillowRepository.GetBedrooms(bedrooms);

        if (apartments == null)
        {
            return NotFound();
        }

        return Ok(apartments);
    }

    // [HttpPost("favorite/{id}")]
    // public async Task<ActionResult<ApartmentProperty>> AddToFavorites(string id)
    // {
    //     var apartment = await _zillowRepository.AddToFavorites(id);

    //     if (apartment == null)
    //     {
    //         return NotFound();
    //     }

    //     return Ok(apartment);
    // }

    // [HttpDelete("favorite/{id}")]
    // public async Task<ActionResult<ApartmentProperty>> RemoveFromFavorites(string id)
    // {
    //     var apartment = await _zillowRepository.RemoveFromFavorites(id);

    //     if (apartment == null)
    //     {
    //         return NotFound();
    //     }

    //     return Ok(apartment);
    // }
}
// This was the first version of controlller without dependencey injection. I had no abstration of view and database. Therefore,  "Repo" and "Interface" folder was not required. 



// using FindMeApartmentsWebApp.Models;
// using Microsoft.AspNetCore.Mvc;
// using Newtonsoft.Json;
// using System.Linq;

// namespace FindMeApartmentsWebApp.Controllers;

// [ApiController]
// [Route("[controller]")]
// public class ZillowController : ControllerBase
// {
//     List<ApartmentProperty> properties;
//     List<ApartmentProperty> favoriteProperties;

//     public ZillowController()
//     {
//         // Read the JSON data 
//         string jsonData = System.IO.File.ReadAllText("ZillowData.json");

//         // Deserialize the JSON data into a ApartmentProperty object
//         properties = JsonConvert.DeserializeObject<List<ApartmentProperty>>(jsonData);

//         favoriteProperties = new List<ApartmentProperty>();
//     }

//     // GET all the apartments
//     [HttpGet]
//     public ActionResult<List<ApartmentProperty>> GetAll()
//     {
//         return Ok(properties);

//     }

//     [HttpGet("{addressZipcode}")]
//     public IActionResult GetZipcode(string addressZipcode)
//     {
//         // Filter the properties to only include those with a given addressZipcode
//         var filtered_zipCode = properties.Where(p => p.AddressZipcode == addressZipcode).ToList();

//         return Ok(JsonConvert.SerializeObject(filtered_zipCode));

//     }

//     [HttpGet("bedrooms/{bedrooms}")]
//     public IActionResult GetBedrooms(string bedrooms)
//     {
//         double bedroomsDouble = Double.Parse(bedrooms);
//         var filtered_bedroom = properties.Where(p => p.Beds == bedroomsDouble).ToList();

//         return Ok(filtered_bedroom);
//     }

//     [HttpPost("favorite/{id}")]
//     public IActionResult AddToFavorites(string id)
//     {
//         var property = properties.FirstOrDefault(p => p.Id == id);
        
//         if (property == null)
//         {
//             return NotFound();
//         }

//         if (System.IO.File.Exists("Favorites.json"))
//         {
//             string json = System.IO.File.ReadAllText("Favorites.json");
//             favoriteProperties = JsonConvert.DeserializeObject<List<ApartmentProperty>>(json);
//         }

//         favoriteProperties.Add(property);

//         string updatedJson = JsonConvert.SerializeObject(favoriteProperties);
       
//         System.IO.File.WriteAllText("Favorites.json", updatedJson);

//         return Ok(JsonConvert.SerializeObject(property));


//     }

//     [HttpDelete("favorite/{id}")]
//     public IActionResult RemoveFromFavorites(string id)
//     {
//         string jsonData = System.IO.File.ReadAllText("Favorites.json");

//         // Deserialize the JSON data into a ApartmentProperty object
//         var favoriteProperties = JsonConvert.DeserializeObject<List<ApartmentProperty>>(jsonData);

//         var propertyToBeDeleted = favoriteProperties.First(p => p.Id == id);
//         if (propertyToBeDeleted != null)
//         {
//             favoriteProperties.Remove(propertyToBeDeleted);
//         }
//         else
//         {
//             return NotFound();
//         }
//         jsonData = JsonConvert.SerializeObject(favoriteProperties);
//         System.IO.File.WriteAllText("Favorites.json", jsonData);

//         return Ok(JsonConvert.SerializeObject(propertyToBeDeleted));
//     }

// }
