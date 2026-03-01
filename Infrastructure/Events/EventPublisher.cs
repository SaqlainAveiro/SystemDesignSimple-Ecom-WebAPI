using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Events;

public class EventPublisher : IEventPublisher
{
    private readonly IServiceProvider _serviceProvider;

    public EventPublisher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task PublishAsync<TEvent>(TEvent @event)
    {
        var eventHandlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();

        foreach (var eventHandler in eventHandlers)
        {
            try
            {
                await eventHandler.HandleEventAsync(@event);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
