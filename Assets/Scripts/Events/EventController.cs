using System;

public class EventController
{
    public event Action baseEvent;
    public void InvokeEvent() => baseEvent?.Invoke();
    public void AddListener(Action listener) => baseEvent += listener;
    public void RemoveListener(Action listener) => baseEvent -= listener;
}
public class EventController<T>
{
    public event Action<T> baseEvent;

    public void InvokeEvent(T arg) => baseEvent?.Invoke(arg);
    public void AddListener(Action<T> listener) => baseEvent += listener;
    public void RemoveListener(Action<T> listener) => baseEvent -= listener;
}
