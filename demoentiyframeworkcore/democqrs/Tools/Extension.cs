using democqrs.Events;

namespace democqrs.Tools;

public static class Extension
{

    public static void AddEvents(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IEventPublisher,InMemoryEventBus>();
    }
}