using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler(IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Order Created Event Handled: {DomainEvent}", domainEvent.GetType().Name);
            //BONUS: We can perform some action here, like sending a notification, connecting to shipments, payment, etc.
            //by publishing the integration events via message broker to other microservices.

            if(await featureManager.IsEnabledAsync("OrderFullfillment"))
            {
                var orderCreated = domainEvent.order.ToOrderDto();
                await publishEndpoint.Publish(orderCreated, cancellationToken);
            }   
        }
    }
}
