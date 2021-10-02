using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeSharing.Domain.Entities
{
    public class Bicycle
    {
        public Guid BicycleId { get; set; }
        public DateTime ProductionDate { get; set; }
    }
}
