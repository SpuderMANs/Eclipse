namespace Eclipse.Events.Features
{
    using Eclipse.API.Features;
    using System;
    using System.Linq;

    public class Event<T>
    {
        private event Action<T> InnerEvent;

        public static Event<T> operator +(Event<T> e, Action<T> handler)
        {
            e.Subscribe(handler);
            return e;
        }

        public static Event<T> operator -(Event<T> e, Action<T> handler)
        {
            e.Unsubscribe(handler);
            return e;
        }

        public void Subscribe(Action<T> handler) => InnerEvent += handler;
        public void Unsubscribe(Action<T> handler) => InnerEvent -= handler;

        public void Invoke(T arg)
        {
            if (InnerEvent == null) return;

            foreach (var handler in InnerEvent.GetInvocationList())
            {
                try
                {
                    ((Action<T>)handler)(arg);
                }
                catch (Exception ex)
                {
                    Log.Error($"Method \"{handler.Method.Name}\" of class \"{handler.Method.ReflectedType.FullName}\" caused an exception in event \"{typeof(Event<T>).FullName}\"\n{ex}");
                }
            }
        }
    }
}
