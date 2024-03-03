namespace Flispter.Shared.Contracts.EventBus;

public class EventBus
{
    private Dictionary<string, List<object>> _callbacks = new();

    public void Subscribe<T>(IEventHandler<T> callback)
        where T : IEvent
    {
        string eKey = typeof(T).Name;
        if (_callbacks.ContainsKey(eKey))
            _callbacks[eKey].Add(callback);
        else
            _callbacks.Add(eKey, new List<object>() { callback });
    }

    public void Unsubscribe<T>(IEventHandler<T> callback)
        where T : IEvent
    {
        string eKey = typeof(T).Name;
        if (!_callbacks.ContainsKey(eKey))
            throw new ArgumentException(nameof(callback));
        _callbacks[eKey].Remove(callback);
    }

    public void Invoke<T>(T e)
        where T : IEvent
    {
        string eKey = typeof(T).Name;
        if (_callbacks.ContainsKey(eKey))
            foreach (var callback in _callbacks[eKey])
                (callback as IEventHandler<T>)?.Handler(e);
    }
}