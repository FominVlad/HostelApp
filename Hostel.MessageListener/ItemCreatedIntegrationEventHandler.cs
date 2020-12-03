using EventBus.Base.Standard;
using Hostel.MessageBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hostel.MessageListener
{
    public class ItemCreatedIntegrationEventHandler : IIntegrationEventHandler<ItemCreatedIntegrationEvent>
    {
        public ItemCreatedIntegrationEventHandler()
        {
        }

        public async Task Handle(ItemCreatedIntegrationEvent @event)
        {
            //Handle the ItemCreatedIntegrationEvent event here.
        }
    }
}
