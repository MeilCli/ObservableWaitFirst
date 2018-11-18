using System;

namespace ObservableWaitFirst
{
    public static class ObservableExtension
    {
        public static WaitFirstAwaitable<T> WaitFirst<T>(this IObservable<T> source)
        {
            return new WaitFirstAwaitable<T>(source);
        }
    }
}
