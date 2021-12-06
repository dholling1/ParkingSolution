using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Parking.API.Repositories;
using Parking.Core.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Parking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly ILogger<ParkingController> _logger;
        private readonly IParkingLotRepository _parkingLots;

        public ParkingController(ILogger<ParkingController> logger, IParkingLotRepository parkingLots)
        {
            _logger = logger;
            _parkingLots = parkingLots;
        }

        /// <summary>
        /// Gets a list of all available parking lots
        /// </summary>
        /// <returns>List of parking lot names</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ParkingLot), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var returnValue = await _parkingLots.GetParkingLotsAsync();
                return Ok(returnValue);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Retrieves the current and maximum count for a given parking lot
        /// </summary>
        /// <param name="id">The ID of the parking lot</param>
        /// <returns>The current values for the specified parking lot</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ParkingLot), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var returnValue = await _parkingLots.GetParkingLotAsync(id);
                return Ok(returnValue);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Sets the current value for a parking lot
        /// </summary>
        /// <param name="id"The ID of the parking lot</param>
        /// <param name="value">The value to set the current parking lot value to</param>
        /// <returns>Updated value for the parking lot</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] int newValue)
        {
            try
            {
                var returnValue = await _parkingLots.SetParkingLotCountAsync(id, newValue);
                return Ok(returnValue);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        /// <summary>
        /// Sets the current value for a parking lot
        /// </summary>
        /// <param name="id"The ID of the parking lot</param>
        /// <param name="value">The value to set the current parking lot value to</param>
        /// <returns>Updated value for the parking lot</returns>
        [HttpPut("{id}/increment")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Increment(int id)
        {
            try
            {
                var returnValue = await _parkingLots.IncrementParkingLotCountAsync(id);
                return Ok(returnValue);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Sets the current value for a parking lot
        /// </summary>
        /// <param name="id"The ID of the parking lot</param>
        /// <param name="value">The value to set the current parking lot value to</param>
        /// <returns>Updated value for the parking lot</returns>
        [HttpPut("{id}/decrement")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Decrement(int id)
        {
            try
            {
            var returnValue = await _parkingLots.DecrementParkingLotCountAsync(id);
            return Ok(returnValue);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        protected NotFoundObjectResult NotFound(string controllerAndMethod, KeyNotFoundException exception)
        {
            var msg = exception.ErrorMessage();
            _logger.LogError(controllerAndMethod + $": {msg}");
            return NotFound(msg);
        }

        protected BadRequestObjectResult BadRequest(string controllerAndMethod, Exception exception)
        {
            var msg = exception.ErrorMessage();
            _logger.LogError(controllerAndMethod + $": {msg}");
            return BadRequest(msg);
        }

        protected ConflictObjectResult Conflict(string controllerAndMethod, Exception exception)
        {
            var msg = exception.ErrorMessage();
            _logger.LogError(controllerAndMethod + $": {msg}");
            return Conflict(msg);
        }
    }
}
