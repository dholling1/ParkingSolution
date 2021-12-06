using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Parking.Core.Models;

namespace Parking.DataSource
{
    public class DataGenerator
    {
        // Attributed to https://exceptionnotfound.net/ef-core-inmemory-asp-net-core-store-database/#:~:text=Using%20EF%20Core%27s%20InMemory%20Provider%20To%20Store%20A,...%205%20Important%20Caveats.%20...%206%20Summary.%20
        public static void Initialize(ParkingDBContext context)
        {
                if (context.ParkingLots.Any())
                {
                    return;
                }

                context.ParkingLots.AddRange(
                    new ParkingLot
                    {
                        Id = 1,
                        Name = "Lot A",
                        MaximumCapacity = 65,
                        CurrentCount = 32
                    },
                    new ParkingLot
                    {
                        Id = 2,
                        Name = "Executive Lot B",
                        MaximumCapacity = 20,
                        CurrentCount = 3
                    },
                    new ParkingLot
                    {
                        Id = 3,
                        Name = "Lot D",
                        MaximumCapacity = 121,
                        CurrentCount = 44
                    },
                    new ParkingLot
                    {
                        Id = 4,
                        Name = "Lot E",
                        MaximumCapacity = 44,
                        CurrentCount = 12
                    },
                    new ParkingLot
                    {
                        Id = 5,
                        Name = "Lot O",
                        MaximumCapacity = 67,
                        CurrentCount = 33
                    },
                    new ParkingLot
                    {
                        Id = 6,
                        Name = "Lot Z",
                        MaximumCapacity = 33,
                        CurrentCount = 0
                    });

                context.SaveChanges();
            }
    }
}
