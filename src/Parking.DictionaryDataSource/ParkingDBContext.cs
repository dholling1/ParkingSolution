using Microsoft.EntityFrameworkCore;
using Parking.Core.Models;

namespace Parking.DataSource
{
    public class ParkingDBContext : DbContext
    {
        public ParkingDBContext(DbContextOptions<ParkingDBContext> options)
            : base(options) { }

        public DbSet<ParkingLot> ParkingLots { get; set; }
    }
}
