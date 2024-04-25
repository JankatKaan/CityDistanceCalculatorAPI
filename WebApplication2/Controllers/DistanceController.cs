using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace DistanceCalculator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DistanceController : ControllerBase
    {
        private static readonly Dictionary<string, City> CityData = new Dictionary<string, City>
        {
            //This is the list of cities in Turkey (Türkiye) with cordinates.
            {"Adana", new City { Name = "Adana", Latitude = 37.00000000, Longitude = 35.32133330 }},
            {"Adýyaman", new City { Name = "Adýyaman", Latitude = 37.76416670, Longitude = 38.27616670 }},
            {"Afyon", new City { Name = "Afyon", Latitude = 38.76376000, Longitude = 30.54034000 }},
            {"Aðrý", new City { Name = "Aðrý", Latitude = 39.72166670, Longitude = 43.05666670 }},
            {"Amasya", new City { Name = "Amasya", Latitude = 40.65000000, Longitude = 35.83333330 }},
            {"Ankara", new City { Name = "Ankara", Latitude = 39.92077000, Longitude = 32.85411000 }},
            {"Antalya", new City { Name = "Antalya", Latitude = 36.88414000, Longitude = 30.70563000 }},
            {"Artvin", new City { Name = "Artvin", Latitude = 41.18333330, Longitude = 41.81666670 }},
            {"Aydýn", new City { Name = "Aydýn", Latitude = 37.84440000, Longitude = 27.84580000 }},
            {"Balýkesir", new City { Name = "Balýkesir", Latitude = 39.64836900, Longitude = 27.88261000 }},
            {"Bilecik", new City { Name = "Bilecik", Latitude = 40.15013100, Longitude = 29.98306100 }},
            {"Bingöl", new City { Name = "Bingöl", Latitude = 38.88534900, Longitude = 40.49829100 }},
            {"Bitlis", new City { Name = "Bitlis", Latitude = 38.40000000, Longitude = 42.11666670 }},
            {"Bolu", new City { Name = "Bolu", Latitude = 40.73947900, Longitude = 31.61156100 }},
            {"Burdur", new City { Name = "Burdur", Latitude = 37.72690900, Longitude = 30.28887600 }},
            {"Bursa", new City { Name = "Bursa", Latitude = 40.18257000, Longitude = 29.06687000 }},
            {"Çanakkale", new City { Name = "Çanakkale", Latitude = 40.15531200, Longitude = 26.41416000 }},
            {"Çankýrý", new City { Name = "Çankýrý", Latitude = 40.60000000, Longitude = 33.61666670 }},
            {"Çorum", new City { Name = "Çorum", Latitude = 40.55055560, Longitude = 34.95555560 }},
            {"Denizli", new City { Name = "Denizli", Latitude = 37.77652000, Longitude = 29.08639000 }},
            {"Diyarbakýr", new City { Name = "Diyarbakýr", Latitude = 37.91441000, Longitude = 40.23062900 }},
            {"Edirne", new City { Name = "Edirne", Latitude = 41.66666670, Longitude = 26.56666670 }},
            {"Elazýð", new City { Name = "Elazýð", Latitude = 38.68096900, Longitude = 39.22639800 }},
            {"Erzincan", new City { Name = "Erzincan", Latitude = 39.75000000, Longitude = 39.50000000 }},
            {"Erzurum", new City { Name = "Erzurum", Latitude = 39.90431890 , Longitude = 41.26788530 }},
            {"Eskiþehir", new City { Name = "Eskiþehir", Latitude = 39.78430200, Longitude = 30.51922000 }},
            {"Gaziantep", new City { Name = "Gaziantep", Latitude = 37.06622000, Longitude = 37.38332000 }},
            {"Giresun", new City { Name = "Giresun", Latitude = 40.91281100, Longitude = 38.38953000 }},
            {"Gümüþhane", new City { Name = "Gümüþhane", Latitude = 40.46027780, Longitude = 39.48138890 }},
            {"Hakkari", new City { Name = "Hakkari", Latitude = 37.58333330, Longitude = 43.73333330 }},
            {"Hatay", new City { Name = "Hatay", Latitude = 36.40184880, Longitude = 36.34980970 }},
            {"Isparta", new City { Name = "Isparta", Latitude = 37.76666670, Longitude = 30.55000000 }},
            {"Mersin", new City { Name = "Mersin", Latitude = 36.80000000, Longitude = 34.63333330 }},
            {"Ýstanbul", new City { Name = "Ýstanbul", Latitude = 41.00527000, Longitude = 28.97696000 }},
            {"Ýzmir", new City { Name = "Ýzmir", Latitude = 38.41885000, Longitude = 27.12872000 }},
            {"Kars", new City { Name = "Kars", Latitude = 40.59267000, Longitude = 43.07783100 }},
            {"Kastamonu", new City { Name = "Kastamonu", Latitude = 41.38871000, Longitude = 33.78273000 }},
            {"Kayseri", new City { Name = "Kayseri", Latitude = 38.73333330, Longitude = 35.48333330 }},
            {"Kýrklareli", new City { Name = "Kýrklareli", Latitude = 41.73333330, Longitude = 27.21666670 }},
            {"Kýrþehir", new City { Name = "Kýrþehir", Latitude = 39.15000000, Longitude = 34.16666670 }},
            {"Kocaeli", new City { Name = "Kocaeli", Latitude = 40.85327040, Longitude = 29.88152030 }},
            {"Konya", new City { Name = "Konya", Latitude = 37.86666670, Longitude = 32.48333330 }},
            {"Kütahya", new City { Name = "Kütahya", Latitude = 39.41666670, Longitude = 29.98333330 }},
            {"Malatya", new City { Name = "Malatya", Latitude = 38.35519000, Longitude = 38.30946000 }},
            {"Manisa", new City { Name = "Manisa", Latitude = 38.61909900, Longitude = 27.42892100 }},
            {"Kahramanmaraþ", new City { Name = "Kahramanmaraþ", Latitude = 37.58333330, Longitude = 36.93333330 }},
            {"Mardin", new City { Name = "Mardin", Latitude = 37.31223610, Longitude = 40.73511200 }},
            {"Muðla", new City { Name = "Muðla", Latitude = 37.21527780, Longitude = 28.36361110 }},
            {"Muþ", new City { Name = "Muþ", Latitude = 38.74329260, Longitude = 41.50648230 }},
            {"Nevþehir", new City { Name = "Nevþehir", Latitude = 38.62442000, Longitude = 34.72396900 }},
            {"Niðde", new City { Name = "Niðde", Latitude = 37.96666670, Longitude = 34.68333330 }},
            {"Ordu", new City { Name = "Ordu", Latitude = 40.98333330, Longitude = 37.88333330 }},
            {"Rize", new City { Name = "Rize", Latitude = 41.02005000, Longitude = 40.52344900 }},
            {"Sakarya", new City { Name = "Sakarya", Latitude = 40.75687930, Longitude = 30.37813800 }},
            {"Samsun", new City { Name = "Samsun", Latitude = 41.29278200, Longitude = 36.33128000 }},
            {"Siirt", new City { Name = "Siirt", Latitude = 37.94429000, Longitude = 41.93288000 }},
            {"Sinop", new City { Name = "Sinop", Latitude = 42.02642220, Longitude = 35.15507450 }},
            {"Sivas", new City { Name = "Sivas", Latitude = 39.74766200, Longitude = 37.01787900 }},
            {"Tekirdað", new City { Name = "Tekirdað", Latitude = 40.98333330, Longitude = 27.51666670 }},
            {"Tokat", new City { Name = "Tokat", Latitude = 40.31666670, Longitude = 36.55000000 }},
            {"Trabzon", new City { Name = "Trabzon", Latitude = 41.00000000, Longitude = 39.73333330 }},
            {"Tunceli", new City { Name = "Tunceli", Latitude = 39.10798680, Longitude = 39.54016720 }},
            {"Þanlýurfa", new City { Name = "Þanlýurfa", Latitude = 37.15000000, Longitude = 38.80000000 }},
            {"Uþak", new City { Name = "Uþak", Latitude = 38.68230100, Longitude = 29.40819000 }},
            {"Van", new City { Name = "Van", Latitude = 38.49416670, Longitude = 43.38000000 }},
            {"Yozgat", new City { Name = "Yozgat", Latitude = 39.82000000, Longitude = 34.80444440 }},
            {"Zonguldak", new City { Name = "Zonguldak", Latitude = 41.45640900, Longitude = 31.79873100 }},
            {"Aksaray", new City { Name = "Aksaray", Latitude = 38.36869000, Longitude = 34.03698000 }},
            {"Bayburt", new City { Name = "Bayburt", Latitude = 40.25516900, Longitude = 40.22488000 }},
            {"Karaman", new City { Name = "Karaman", Latitude = 37.17593000, Longitude = 33.22874800 }},
            {"Kýrýkkale", new City { Name = "Kýrýkkale", Latitude = 39.84682100, Longitude = 33.51525100 }},
            {"Batman", new City { Name = "Batman", Latitude = 37.88116800, Longitude = 41.13509000 }},
            {"Þýrnak", new City { Name = "Þýrnak", Latitude = 37.51638890, Longitude = 42.46111110 }},
            {"Bartýn", new City { Name = "Bartýn", Latitude = 41.63444440, Longitude = 32.33750000 }},
            {"Ardahan", new City { Name = "Ardahan", Latitude = 41.11048100, Longitude = 42.70217100 }},
            {"Iðdýr", new City { Name = "Iðdýr", Latitude = 39.91666670, Longitude = 44.03333330 }},
            {"Yalova", new City { Name = "Yalova", Latitude = 40.65000000, Longitude = 29.26666670 }},
            {"Karabük", new City { Name = "Karabük", Latitude = 41.20000000, Longitude = 32.63333330 }},
            {"Kilis", new City { Name = "Kilis", Latitude = 36.71839900, Longitude = 37.12122000 }},
            {"Osmaniye", new City { Name = "Osmaniye", Latitude = 37.06805000, Longitude = 36.26158900 }},
            {"Düzce", new City { Name = "Düzce", Latitude = 40.84384900, Longitude = 31.15654000 }}
        };

        [HttpGet]
        [Route("Calculate")]
        public ActionResult<double> CalculateDistance(string city1, string city2)
        {
            if (!CityData.ContainsKey(city1) || !CityData.ContainsKey(city2))
            {
                //Returns bad request if request city not found.
                return BadRequest("Geçersiz þehir adý.");
            }

            var city1Coords = CityData[city1];
            var city2Coords = CityData[city2];

            var distance = CalculateDistance(city1Coords.Latitude, city1Coords.Longitude, city2Coords.Latitude, city2Coords.Longitude);

            return Ok(distance);
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