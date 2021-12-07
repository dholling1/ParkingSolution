﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parking.Core.Interfaces;
using Parking.Core.Models;

namespace Parking.DataSource
{
    public class ConcurrentDictionary : IParkingDataSource
    {
        private ConcurrentDictionary<int, ParkingLot> _parkingLots;

        public ConcurrentDictionary()
        {
            _parkingLots = new ConcurrentDictionary<int, ParkingLot>();
            _parkingLots.TryAdd(1, new ParkingLot() { Id = 1, Name = "Lot A", MaximumCapacity = 100, CurrentCount = 25 });
            _parkingLots.TryAdd(2, new ParkingLot() { Id = 2, Name = "Lot B", MaximumCapacity = 30, CurrentCount = 30 });
            _parkingLots.TryAdd(3, new ParkingLot() { Id = 3, Name = "Lot E", MaximumCapacity = 60, CurrentCount = 12 });
            _parkingLots.TryAdd(4, new ParkingLot() { Id = 3, Name = "Lot F", MaximumCapacity = 40, CurrentCount = 24 });
            _parkingLots.TryAdd(5, new ParkingLot() { Id = 3, Name = "Lot Z", MaximumCapacity = 50, CurrentCount = 0 });
        }

        public async Task<IEnumerable<ParkingLot>> GetAllParkingLots()
        {
            return _parkingLots.Values.ToList();
        }

        public async Task<ParkingLot> GetParkingLotAsync(int id)
        {
            if (_parkingLots.ContainsKey(id))
            {
                return _parkingLots[id];
            }

            throw new KeyNotFoundException($"Parking lot {id} not found");
        }

        public async Task<int> SetParkingLotCountAsync(int id, int count)
        {
            if (_parkingLots.ContainsKey(id))
            {
                if (count > _parkingLots[id].MaximumCapacity)
                    throw new InvalidOperationException("Cannot exceed maximum parking lot capacity!");

                if (count < 0)
                {
                    // we can "heal" by resetting to 0, but we should never be negative unless racy-race
                    throw new InvalidOperationException("Cannot have a count of less than 0!");
                }

                _parkingLots[id].CurrentCount = count;
                return count;
            }

            throw new KeyNotFoundException($"Parking lot {id} not found");
        }

        public async Task<int> IncrementParkingLotCountAsync(int id)
        {
            if (_parkingLots.ContainsKey(id))
            {
                if (_parkingLots[id].CurrentCount >= _parkingLots[id].MaximumCapacity)
                    throw new InvalidOperationException("Cannot exceed maximum parking lot capacity!");

                _parkingLots[id].CurrentCount = _parkingLots[id].CurrentCount + 1;
                return _parkingLots[id].CurrentCount;
            }

            throw new KeyNotFoundException($"Parking lot {id} not found");
        }

        public async Task<int> DecrementParkingLotCountAsync(int id)
        {
            if (_parkingLots.ContainsKey(id))
            {
                if (_parkingLots[id].CurrentCount <= 0)
                {
                    // we can "heal" by resetting to 0, but we should never be negative unless racy-race
                    throw new InvalidOperationException("Cannot have a count of less than 0!");
                }

                _parkingLots[id].CurrentCount = _parkingLots[id].CurrentCount - 1;
                return _parkingLots[id].CurrentCount;
            }

            throw new KeyNotFoundException($"Parking lot {id} not found");
        }
    }
}
