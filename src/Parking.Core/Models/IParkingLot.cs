namespace Parking.Core.Models
{
    public interface IParkingLot
    {
        int Id { get; set; }
        string Name { get; set; }
        int MaximumCapacity { get; set; }
        int CurrentCount { get; set; }
    }
}