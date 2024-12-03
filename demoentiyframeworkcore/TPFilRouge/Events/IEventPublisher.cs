namespace TPFilRouge.Events;

public interface IEventPublisher
{
    Task PublishAsync<T>(T @event);
    public void Subscribe(Func<object, Task> handler);
}