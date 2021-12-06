using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parking.Core.Interfaces;
using Parking.Core.Models;
using Parking.DataSource;

namespace Parking.API.Repositories
{
    public class ParkingLotRepository : IParkingLotRepository
    {
        private IParkingDataSource _dataSource;

        public ParkingLotRepository(IParkingDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public async Task<IEnumerable<ParkingLot>> GetParkingLotsAsync()
        {
            return await _dataSource.GetAllParkingLots();
        }

        public async Task<ParkingLot> GetParkingLotAsync(int id)
        {
            return await _dataSource.GetParkingLotAsync(id);
        }

        public async Task<int> SetParkingLotCountAsync(int id, int count)
        {
            return await _dataSource.SetParkingLotCountAsync(id, count);
        }

        public async Task<int> IncrementParkingLotCountAsync(int id)
        {
            return await _dataSource.IncrementParkingLotCountAsync(id);
        }

        public async Task<int> DecrementParkingLotCountAsync(int id)
        {
            return await _dataSource.DecrementParkingLotCountAsync(id);
        }
    }
}
