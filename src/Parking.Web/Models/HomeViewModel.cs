using Parking.Core.Models;
using System.Collections.Generic;

namespace Parking.Web.Models
{
    public class HomeViewModel
    {
        public List<ParkingLot> Lots { get; set; }
        public int SelectedLot { get; set; }
    }
}
