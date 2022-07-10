using Autofac;
using OnlineStore.Catalog.API.IntegrationEvents;
using OnlineStore.EventBus;
using OnlineStore.EventBus.Abstractions;
using OnlineStore.EventBus.RabbitMQ;
using RabbitMQ.Client;

namespace OnlineStore.Catalog.API.Configurations.Registrars
{
    public class EventBusRegistrar : IRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {

            builder.Services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var subscriptionClientName = builder.Configuration["SubscriptionClientName"];
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(builder.Configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(builder.Configuration["EventBusRetryCount"]);
                }

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });

            builder.Services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = builder.Configuration["EventBusConnection"],
                    DispatchConsumersAsync = true
                };

                var retryCount = 5;
                if (!string.IsNullOrEmpty(builder.Configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(builder.Configuration["EventBusRetryCount"]);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            builder.Services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();

            builder.Services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        }
    }
}
