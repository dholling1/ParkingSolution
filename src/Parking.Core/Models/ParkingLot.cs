namespace Parking.Core.Models
{
    public class ParkingLot : IParkingLot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaximumCapacity { get; set; }
        public int CurrentCount { get; set; }
    }
}
