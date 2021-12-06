using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Parking.Core.Interfaces;
using Parking.Core.Models;

namespace Parking.DataSource
{
    public class EFCore : IParkingDataSource
    {
        private ParkingDBContext _context;

        public EFCore(ParkingDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ParkingLot>> GetAllParkingLots()
        {
            var lots = await _context.ParkingLots.ToListAsync();

            return lots;
        }

        public async Task<ParkingLot> GetParkingLotAsync(int id)
        {
            var lot = await _context.ParkingLots.Where(x => x.Id == id).FirstAsync();

            if (lot is null)
            {
                throw new KeyNotFoundException($"Unable to find parking lot {id}");
            }

            return lot;
        }

        public async Task<int> SetParkingLotCountAsync(int id, int count)
        {
            var lot = await GetParkingLotAsync(id);

            if (count >= lot.MaximumCapacity)
                throw new InvalidOperationException("Cannot exceed maximum parking lot capacity!");

            lot.CurrentCount = count;

            await _context.SaveChangesAsync();

            lot = await GetParkingLotAsync(id);

            return lot.CurrentCount;
        }

        public async Task<int> IncrementParkingLotCountAsync(int id)
        {
            ParkingLot lot;

            // Simplistic loop to detect a concurrency iss and retry until it succeeds.  VERY MUCH not for production use
            var saved = false;
            while (!saved)
            {
                try
                {
                    lot = await GetParkingLotAsync(id);

                    if (lot.CurrentCount >= lot.MaximumCapacity)
                        throw new InvalidOperationException("Cannot exceed maximum parking lot capacity!");

                    lot.CurrentCount = lot.CurrentCount + 1;
                    await _context.SaveChangesAsync();

                    saved = true;
                }
                catch (DbUpdateConcurrencyException e)
                {
                }
                catch (Exception e)
                {
                    // It's something else, so bail
                    saved = true;
                }
            }
            lot = await GetParkingLotAsync(id);
            return lot.CurrentCount;
        }

        public async Task<int> DecrementParkingLotCountAsync(int id)
        {
            ParkingLot lot;

            // Simplistic loop to detect a concurrency iss and retry until it succeeds.  VERY MUCH not for production use
            var saved = false;
            while (!saved)
            {
                try
                {
                    lot = await GetParkingLotAsync(id);
                    if (lot.CurrentCount <= 0)
                    {
                        // we can "heal" by resetting to 0, but we should never be negative unless racy-race
                        throw new InvalidOperationException("Cannot have a count of less than 0!");
                    }

                    lot.CurrentCount = lot.CurrentCount - 1;
                    await _context.SaveChangesAsync();
                    
                    saved = true;
                }
                catch (DbUpdateConcurrencyException e)
                {
                }
                catch (Exception e)
                {
                    // It's something else, so bail
                    saved = true;
                }
            }
            lot = await GetParkingLotAsync(id);
            return lot.CurrentCount;
        }

        private async Task HandleCountUpdateMethod(string typeOfUpdate, int newValue)
        {
            var saved = false;
            while (!saved)
            {
                try
                {
                    await _context.SaveChangesAsync();
                    saved = true;
                }
                catch (DbUpdateConcurrencyException e)
                {
                }
            }
        }
    }
}
