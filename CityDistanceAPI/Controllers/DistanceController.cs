using Microsoft.AspNetCore.Mvc;
using CityDistanceAPI.Models;
using CityDistanceAPI.Data;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CityDistanceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DistanceController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        public DistanceController(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient();
        }


        [HttpGet]
        [Route("Calculate")]
        public async Task<ActionResult<double>> CalculateDistance(string city1, string city2)
        {
            City? c1 = await _context.Cities.FirstOrDefaultAsync<City>(x => x.Name == city1);
            City? c2 = await _context.Cities.FirstOrDefaultAsync<City>(x => x.Name == city2);
            if (c1 == null)
            {
                c1 = await GetCityFromThirdPartyAPI(city1);
                await _context.Cities.AddAsync(c1);
            }
            if (c2 == null)
            {
                c2 = await GetCityFromThirdPartyAPI(city2);
                await _context.Cities.AddAsync(c2);
            }
            await _context.SaveChangesAsync();
            var distance = CalculateDistance(c1.Latitude, c1.Longitude, c2.Latitude, c2.Longitude);

            return Ok(distance);
        }
        private async Task<City> GetCityFromThirdPartyAPI(string name)
        {
            string path = "https://api.maptiler.com/geocoding/" + name + ".json?key=yourkeyhere";
            var cityJson = await _httpClient.GetStringAsync(path);
            dynamic? city = JsonConvert.DeserializeObject(cityJson);
            return new City() { Id = Guid.NewGuid(), Name = name, Longitude = city.features[0].center[0], Latitude = city.features[0].center[1] };
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double EarthRadius = 6371; // Earth radius in kilometers

            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var distance = EarthRadius * c;
            return distance;
        }

        private double ToRadians(double degree)
        {
            return degree * Math.PI / 180;
        }
    }
}
