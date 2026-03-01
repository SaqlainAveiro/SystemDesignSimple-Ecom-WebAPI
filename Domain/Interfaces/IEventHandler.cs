namespace Domain.Interfaces;

/// <summary>
/// Interface for event handlers
/// </summary>
/// <typeparam name="TEvent">Type of the event</typeparam>
public interface IEventHandler<in TEvent>
{
    /// <summary>
    /// Handle event
    /// </summary>
    /// <param name="eventMessage">Event</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task HandleEventAsync(TEvent eventMessage);
}