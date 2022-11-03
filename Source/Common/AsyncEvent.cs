﻿namespace Common
{
    /// <summary>
    ///     Represents an asynchronous event container.
    /// </summary>
    /// <typeparam name="T">The delegate to invoke.</typeparam>
    public class AsyncEvent<T>
        where T : class
    {
        private readonly object _subLock = new();

        private readonly List<T> _subscriptions;

        /// <summary>
        ///     Checks if the event has any subscribers.
        /// </summary>
        public bool HasSubscribers
            => _subscriptions.Count is not 0;

        /// <summary>
        ///     The subscriptions to this event.
        /// </summary>
        public IReadOnlyList<T> Subscriptions
            => _subscriptions;

        /// <summary>
        ///     Creates a new <see cref="AsyncEvent{T}"/>.
        /// </summary>
        public AsyncEvent()
            => _subscriptions = new();

        /// <summary>
        ///     Adds a subscriber to the event.
        /// </summary>
        /// <param name="subscriber"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Add(T subscriber)
        {
            if (subscriber is null)
                throw new ArgumentNullException(nameof(subscriber));

            lock (_subLock)
                _subscriptions.Add(subscriber);
        }

        /// <summary>
        ///     Removes a subscriber from the event.
        /// </summary>
        /// <param name="subscriber"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Remove(T subscriber)
        {
            if (subscriber is null)
                throw new ArgumentNullException(nameof(subscriber));

            lock (_subLock)
                _subscriptions.Remove(subscriber);
        }
    }

    public static class AsyncEvent
    {
        public static async Task InvokeAsync(this AsyncEvent<Func<Task>> eventHandler)
        {
            var subscribers = eventHandler.Subscriptions;
            for (int i = 0; i < subscribers.Count; i++)
                await subscribers[i].Invoke().ConfigureAwait(false);
        }

        public static async Task InvokeAsync<T>(this AsyncEvent<Func<T, Task>> eventHandler, T arg)
        {
            var subscribers = eventHandler.Subscriptions;
            for (int i = 0; i < subscribers.Count; i++)
                await subscribers[i].Invoke(arg).ConfigureAwait(false);
        }

        public static async Task InvokeAsync<T1, T2>(this AsyncEvent<Func<T1, T2, Task>> eventHandler, T1 arg1, T2 arg2)
        {
            var subscribers = eventHandler.Subscriptions;
            for (int i = 0; i < subscribers.Count; i++)
                await subscribers[i].Invoke(arg1, arg2).ConfigureAwait(false);
        }

        public static async Task InvokeAsync<T1, T2, T3>(this AsyncEvent<Func<T1, T2, T3, Task>> eventHandler, T1 arg1, T2 arg2, T3 arg3)
        {
            var subscribers = eventHandler.Subscriptions;
            for (int i = 0; i < subscribers.Count; i++)
                await subscribers[i].Invoke(arg1, arg2, arg3).ConfigureAwait(false);
        }

        public static async Task InvokeAsync<T1, T2, T3, T4>(this AsyncEvent<Func<T1, T2, T3, T4, Task>> eventHandler, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            var subscribers = eventHandler.Subscriptions;
            for (int i = 0; i < subscribers.Count; i++)
                await subscribers[i].Invoke(arg1, arg2, arg3, arg4).ConfigureAwait(false);
        }

        public static async Task InvokeAsync<T1, T2, T3, T4, T5>(this AsyncEvent<Func<T1, T2, T3, T4, T5, Task>> eventHandler, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            var subscribers = eventHandler.Subscriptions;
            for (int i = 0; i < subscribers.Count; i++)
                await subscribers[i].Invoke(arg1, arg2, arg3, arg4, arg5).ConfigureAwait(false);
        }
    }
}
