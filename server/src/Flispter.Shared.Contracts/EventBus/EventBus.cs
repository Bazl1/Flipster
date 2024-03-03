namespace Flispter.Shared.Contracts.EventBus;
using Microsoft.Extensions.DependencyInjection;

public class EventBus(
    IServiceProvider _serviceProvider)
{
    private Dictionary<string, List<Type>> _handlers = new();

    public void Subscribe<T, TH>()
        where T : IEvent
        where TH : IEventHandler<T>
    {
        string eKey = typeof(T).Name;
        if (!_handlers.ContainsKey(eKey))
            _handlers.Add(eKey, new());
        _handlers[eKey].Add(typeof(TH));
    }

    public void Unsubscribe<T, TH>()
        where T : IEvent
        where TH : IEventHandler<T>
    {
        string eKey = typeof(T).Name;
        if (!_handlers.ContainsKey(eKey))
            throw new ArgumentException(typeof(T).Name);
        _handlers[eKey].Remove(typeof(TH));
    }

    public void Publish<T>(T @event)
        where T : IEvent
    {
        string eKey = typeof(T).Name;
        if (_handlers.ContainsKey(eKey))
            foreach (Type handlerType in _handlers[eKey])
                (_serviceProvider.GetRequiredService(handlerType) as IEventHandler<T>)?.Handler(@event);
    }
}