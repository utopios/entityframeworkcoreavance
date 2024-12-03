namespace democqrs.Events;

public class InMemoryEventBus : IEventPublisher
{
    private readonly List<Func<object, Task>> _subscribers = new();

    public void Subscribe(Func<object, Task> handler)
    {
        if (handler != null) 
            _subscribers.Add(handler);
    }
    
    public async Task PublishAsync<T>(T @event)
    {
        foreach (var subscriber in _subscribers)
        {
            await subscriber(@event);
        }
    }
}