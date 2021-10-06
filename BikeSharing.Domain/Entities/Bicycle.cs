using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeSharing.Domain.Enums;

namespace BikeSharing.Domain.Entities
{
    public class Bicycle
    {
        public Guid Id { get; set; }
        public DateTime InServiceDate { get; set; }
        public EnumLocation? Location { get; set; }
    }
}
