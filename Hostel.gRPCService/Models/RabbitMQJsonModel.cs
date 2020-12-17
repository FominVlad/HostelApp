using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hostel.gRPCService.Models
{
    public class RabbitMQJsonModel
    {
        public string Method { get; set; }
        public string ObjectJSON { get; set; }
    }
}
