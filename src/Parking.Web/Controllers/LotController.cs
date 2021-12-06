using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Parking.Web.Clients;

namespace Parking.Web.Controllers
{
    public class LotController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IParkingApiClient _client;

        public LotController(ILogger<HomeController> logger, IParkingApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        [HttpGet("lot/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            try
            {
                var lot = await _client.GetParkingLot(id);
                return View(lot);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/");
            }
        }

        [HttpGet("lot/{id}/increment")]
        public async Task<IActionResult> Increment(int id)
        {
            try
            {
                var currentCount = await _client.IncrementParkingLot(id);
                return Json(new { success = true, responseText = currentCount });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { success = false, responseText = "An error occurred updating the count" });

            }
        }

        [HttpGet("lot/{id}/decrement")]
        public async Task<IActionResult> Decrement(int id)
        {
            try
            {
                var currentCount = await _client.DecrementParkingLot(id);
                return Json(new { success = true, responseText = currentCount });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { success = false, responseText = "An error occurred updating the count" });

            }
        }

        [HttpGet("lot/{id}/pollcount")]
        public async Task<IActionResult> PollCount(int id)
        {
            try
            {
                var lot = await _client.GetParkingLot(id);
                return Json(new { success = true, responseText = lot.CurrentCount });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { success = false, responseText = "An error occurred retrieving the count" });

            }
        }

    }
}
