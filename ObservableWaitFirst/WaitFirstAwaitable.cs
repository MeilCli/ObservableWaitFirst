using System;

namespace ObservableWaitFirst
{
    public class WaitFirstAwaitable<T>
    {
        private readonly IObservable<T> source;

        public WaitFirstAwaitable(IObservable<T> source)
        {
            this.source = source;
        }

        public WaitFirstAwaiter<T> GetAwaiter()
        {
            return new WaitFirstAwaiter<T>(source);
        }
    }
}
