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
            //This is the list of cities in Turkey (T�rkiye) with cordinates.
            {"Adana", new City { Name = "Adana", Latitude = 37.00000000, Longitude = 35.32133330 }},
            {"Ad�yaman", new City { Name = "Ad�yaman", Latitude = 37.76416670, Longitude = 38.27616670 }},
            {"Afyon", new City { Name = "Afyon", Latitude = 38.76376000, Longitude = 30.54034000 }},
            {"A�r�", new City { Name = "A�r�", Latitude = 39.72166670, Longitude = 43.05666670 }},
            {"Amasya", new City { Name = "Amasya", Latitude = 40.65000000, Longitude = 35.83333330 }},
            {"Ankara", new City { Name = "Ankara", Latitude = 39.92077000, Longitude = 32.85411000 }},
            {"Antalya", new City { Name = "Antalya", Latitude = 36.88414000, Longitude = 30.70563000 }},
            {"Artvin", new City { Name = "Artvin", Latitude = 41.18333330, Longitude = 41.81666670 }},
            {"Ayd�n", new City { Name = "Ayd�n", Latitude = 37.84440000, Longitude = 27.84580000 }},
            {"Bal�kesir", new City { Name = "Bal�kesir", Latitude = 39.64836900, Longitude = 27.88261000 }},
            {"Bilecik", new City { Name = "Bilecik", Latitude = 40.15013100, Longitude = 29.98306100 }},
            {"Bing�l", new City { Name = "Bing�l", Latitude = 38.88534900, Longitude = 40.49829100 }},
            {"Bitlis", new City { Name = "Bitlis", Latitude = 38.40000000, Longitude = 42.11666670 }},
            {"Bolu", new City { Name = "Bolu", Latitude = 40.73947900, Longitude = 31.61156100 }},
            {"Burdur", new City { Name = "Burdur", Latitude = 37.72690900, Longitude = 30.28887600 }},
            {"Bursa", new City { Name = "Bursa", Latitude = 40.18257000, Longitude = 29.06687000 }},
            {"�anakkale", new City { Name = "�anakkale", Latitude = 40.15531200, Longitude = 26.41416000 }},
            {"�ank�r�", new City { Name = "�ank�r�", Latitude = 40.60000000, Longitude = 33.61666670 }},
            {"�orum", new City { Name = "�orum", Latitude = 40.55055560, Longitude = 34.95555560 }},
            {"Denizli", new City { Name = "Denizli", Latitude = 37.77652000, Longitude = 29.08639000 }},
            {"Diyarbak�r", new City { Name = "Diyarbak�r", Latitude = 37.91441000, Longitude = 40.23062900 }},
            {"Edirne", new City { Name = "Edirne", Latitude = 41.66666670, Longitude = 26.56666670 }},
            {"Elaz��", new City { Name = "Elaz��", Latitude = 38.68096900, Longitude = 39.22639800 }},
            {"Erzincan", new City { Name = "Erzincan", Latitude = 39.75000000, Longitude = 39.50000000 }},
            {"Erzurum", new City { Name = "Erzurum", Latitude = 39.90431890 , Longitude = 41.26788530 }},
            {"Eski�ehir", new City { Name = "Eski�ehir", Latitude = 39.78430200, Longitude = 30.51922000 }},
            {"Gaziantep", new City { Name = "Gaziantep", Latitude = 37.06622000, Longitude = 37.38332000 }},
            {"Giresun", new City { Name = "Giresun", Latitude = 40.91281100, Longitude = 38.38953000 }},
            {"G�m��hane", new City { Name = "G�m��hane", Latitude = 40.46027780, Longitude = 39.48138890 }},
            {"Hakkari", new City { Name = "Hakkari", Latitude = 37.58333330, Longitude = 43.73333330 }},
            {"Hatay", new City { Name = "Hatay", Latitude = 36.40184880, Longitude = 36.34980970 }},
            {"Isparta", new City { Name = "Isparta", Latitude = 37.76666670, Longitude = 30.55000000 }},
            {"Mersin", new City { Name = "Mersin", Latitude = 36.80000000, Longitude = 34.63333330 }},
            {"�stanbul", new City { Name = "�stanbul", Latitude = 41.00527000, Longitude = 28.97696000 }},
            {"�zmir", new City { Name = "�zmir", Latitude = 38.41885000, Longitude = 27.12872000 }},
            {"Kars", new City { Name = "Kars", Latitude = 40.59267000, Longitude = 43.07783100 }},
            {"Kastamonu", new City { Name = "Kastamonu", Latitude = 41.38871000, Longitude = 33.78273000 }},
            {"Kayseri", new City { Name = "Kayseri", Latitude = 38.73333330, Longitude = 35.48333330 }},
            {"K�rklareli", new City { Name = "K�rklareli", Latitude = 41.73333330, Longitude = 27.21666670 }},
            {"K�r�ehir", new City { Name = "K�r�ehir", Latitude = 39.15000000, Longitude = 34.16666670 }},
            {"Kocaeli", new City { Name = "Kocaeli", Latitude = 40.85327040, Longitude = 29.88152030 }},
            {"Konya", new City { Name = "Konya", Latitude = 37.86666670, Longitude = 32.48333330 }},
            {"K�tahya", new City { Name = "K�tahya", Latitude = 39.41666670, Longitude = 29.98333330 }},
            {"Malatya", new City { Name = "Malatya", Latitude = 38.35519000, Longitude = 38.30946000 }},
            {"Manisa", new City { Name = "Manisa", Latitude = 38.61909900, Longitude = 27.42892100 }},
            {"Kahramanmara�", new City { Name = "Kahramanmara�", Latitude = 37.58333330, Longitude = 36.93333330 }},
            {"Mardin", new City { Name = "Mardin", Latitude = 37.31223610, Longitude = 40.73511200 }},
            {"Mu�la", new City { Name = "Mu�la", Latitude = 37.21527780, Longitude = 28.36361110 }},
            {"Mu�", new City { Name = "Mu�", Latitude = 38.74329260, Longitude = 41.50648230 }},
            {"Nev�ehir", new City { Name = "Nev�ehir", Latitude = 38.62442000, Longitude = 34.72396900 }},
            {"Ni�de", new City { Name = "Ni�de", Latitude = 37.96666670, Longitude = 34.68333330 }},
            {"Ordu", new City { Name = "Ordu", Latitude = 40.98333330, Longitude = 37.88333330 }},
            {"Rize", new City { Name = "Rize", Latitude = 41.02005000, Longitude = 40.52344900 }},
            {"Sakarya", new City { Name = "Sakarya", Latitude = 40.75687930, Longitude = 30.37813800 }},
            {"Samsun", new City { Name = "Samsun", Latitude = 41.29278200, Longitude = 36.33128000 }},
            {"Siirt", new City { Name = "Siirt", Latitude = 37.94429000, Longitude = 41.93288000 }},
            {"Sinop", new City { Name = "Sinop", Latitude = 42.02642220, Longitude = 35.15507450 }},
            {"Sivas", new City { Name = "Sivas", Latitude = 39.74766200, Longitude = 37.01787900 }},
            {"Tekirda�", new City { Name = "Tekirda�", Latitude = 40.98333330, Longitude = 27.51666670 }},
            {"Tokat", new City { Name = "Tokat", Latitude = 40.31666670, Longitude = 36.55000000 }},
            {"Trabzon", new City { Name = "Trabzon", Latitude = 41.00000000, Longitude = 39.73333330 }},
            {"Tunceli", new City { Name = "Tunceli", Latitude = 39.10798680, Longitude = 39.54016720 }},
            {"�anl�urfa", new City { Name = "�anl�urfa", Latitude = 37.15000000, Longitude = 38.80000000 }},
            {"U�ak", new City { Name = "U�ak", Latitude = 38.68230100, Longitude = 29.40819000 }},
            {"Van", new City { Name = "Van", Latitude = 38.49416670, Longitude = 43.38000000 }},
            {"Yozgat", new City { Name = "Yozgat", Latitude = 39.82000000, Longitude = 34.80444440 }},
            {"Zonguldak", new City { Name = "Zonguldak", Latitude = 41.45640900, Longitude = 31.79873100 }},
            {"Aksaray", new City { Name = "Aksaray", Latitude = 38.36869000, Longitude = 34.03698000 }},
            {"Bayburt", new City { Name = "Bayburt", Latitude = 40.25516900, Longitude = 40.22488000 }},
            {"Karaman", new City { Name = "Karaman", Latitude = 37.17593000, Longitude = 33.22874800 }},
            {"K�r�kkale", new City { Name = "K�r�kkale", Latitude = 39.84682100, Longitude = 33.51525100 }},
            {"Batman", new City { Name = "Batman", Latitude = 37.88116800, Longitude = 41.13509000 }},
            {"��rnak", new City { Name = "��rnak", Latitude = 37.51638890, Longitude = 42.46111110 }},
            {"Bart�n", new City { Name = "Bart�n", Latitude = 41.63444440, Longitude = 32.33750000 }},
            {"Ardahan", new City { Name = "Ardahan", Latitude = 41.11048100, Longitude = 42.70217100 }},
            {"I�d�r", new City { Name = "I�d�r", Latitude = 39.91666670, Longitude = 44.03333330 }},
            {"Yalova", new City { Name = "Yalova", Latitude = 40.65000000, Longitude = 29.26666670 }},
            {"Karab�k", new City { Name = "Karab�k", Latitude = 41.20000000, Longitude = 32.63333330 }},
            {"Kilis", new City { Name = "Kilis", Latitude = 36.71839900, Longitude = 37.12122000 }},
            {"Osmaniye", new City { Name = "Osmaniye", Latitude = 37.06805000, Longitude = 36.26158900 }},
            {"D�zce", new City { Name = "D�zce", Latitude = 40.84384900, Longitude = 31.15654000 }}
        };

        [HttpGet]
        [Route("Calculate")]
        public ActionResult<double> CalculateDistance(string city1, string city2)
        {
            if (!CityData.ContainsKey(city1) || !CityData.ContainsKey(city2))
            {
                //Returns bad request if request city not found.
                return BadRequest("Ge�ersiz �ehir ad�.");
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