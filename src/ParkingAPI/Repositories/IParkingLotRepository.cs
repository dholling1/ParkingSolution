using System.Collections.Generic;
using System.Threading.Tasks;
using Parking.Core.Models;


namespace Parking.API.Repositories
{
    public interface IParkingLotRepository
    {
        Task<IEnumerable<ParkingLot>> GetParkingLotsAsync();
        Task<ParkingLot> GetParkingLotAsync(int id);
        Task<int> SetParkingLotCountAsync(int id, int count);
        Task<int> IncrementParkingLotCountAsync(int id);
        Task<int> DecrementParkingLotCountAsync(int id);
    }
}