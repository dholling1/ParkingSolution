using System.Collections.Generic;
using System.Threading.Tasks;
using Parking.Core.Models;

namespace Parking.Core.Interfaces
{
    public interface IParkingDataSource
    {
        Task<IEnumerable<ParkingLot>> GetAllParkingLots();
        Task<ParkingLot> GetParkingLotAsync(int id);
        Task<int> SetParkingLotCountAsync(int id, int count);
        Task<int> IncrementParkingLotCountAsync(int id);
        Task<int> DecrementParkingLotCountAsync(int id);
    }
}