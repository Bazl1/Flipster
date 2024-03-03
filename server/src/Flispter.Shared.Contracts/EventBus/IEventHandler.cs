namespace Flispter.Shared.Contracts.EventBus;

public interface IEventHandler<T>
    where T : IEvent
{
    void Handler(T e);
}