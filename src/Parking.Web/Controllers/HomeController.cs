using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Parking.Web.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Parking.Web.Clients;

namespace Parking.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IParkingApiClient _client;

        public HomeController(ILogger<HomeController> logger, IParkingApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            //var parkingLots = new List<ParkingLot>();
            //parkingLots.Add(new ParkingLot() { Id = 1, Name = "Lot A", MaximumCapacity = 100, CurrentCount = 25 });
            //parkingLots.Add(new ParkingLot() { Id = 2, Name = "Lot B", MaximumCapacity = 30, CurrentCount = 30 });
            //parkingLots.Add(new ParkingLot() { Id = 3, Name = "Lot Z", MaximumCapacity = 50, CurrentCount = 0 });

            var parkingLots = await _client.GetParkingLots();
            var model = new HomeViewModel() { Lots  = parkingLots.ToList() };
            
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
