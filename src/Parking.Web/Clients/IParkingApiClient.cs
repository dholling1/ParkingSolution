using System.Collections.Generic;
using System.Threading.Tasks;
using Parking.Core.Models;

namespace Parking.Web.Clients
{
    public interface IParkingApiClient
    {
        Task<IEnumerable<ParkingLot>> GetParkingLots();
        Task<ParkingLot> GetParkingLot(int id);
        Task<int> IncrementParkingLot(int id);
        Task<int> DecrementParkingLot(int id);
    }
}