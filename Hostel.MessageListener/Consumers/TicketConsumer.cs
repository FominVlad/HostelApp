using Hostel.MessageListener.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hostel.MessageListener.Consumers
{
    public class TicketConsumer : IConsumer<Ticket>
    {
        public async Task Consume(ConsumeContext<Ticket> context)
        {
            var data = context.Message;
            //Validate the Ticket Data
            //Store to Database
            //Notify the user via Email / SMS
        }
    }
}
