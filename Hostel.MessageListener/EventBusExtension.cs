using EventBus.Base.Standard;
using Hostel.MessageBroker;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hostel.MessageListener
{
    public static class EventBusExtension
    {
        public static IEnumerable<IIntegrationEventHandler> GetHandlers()
        {
            return new List<IIntegrationEventHandler>
        {
            new ItemCreatedIntegrationEventHandler()
        };
        }

        public static IApplicationBuilder SubscribeToEvents(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<ItemCreatedIntegrationEvent, ItemCreatedIntegrationEventHandler>();

            return app;
        }
    }
}
