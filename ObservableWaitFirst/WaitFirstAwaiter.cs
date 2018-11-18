using System;
using System.Runtime.CompilerServices;

namespace ObservableWaitFirst
{
    public class WaitFirstAwaiter<T> : ICriticalNotifyCompletion
    {
        private IDisposable disposable;
        private T result;
        private Action continuation;

        public bool IsCompleted { get; private set; }

        internal WaitFirstAwaiter(IObservable<T> source)
        {
            disposable = source.Subscribe(x =>
            {
                result = x;
                IsCompleted = true;
                disposable?.Dispose();
                continuation?.Invoke();
            });
            if (IsCompleted)
            {
                disposable?.Dispose();
            }
        }

        public void OnCompleted(Action continuation)
        {
            this.continuation = continuation;
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            this.continuation = continuation;
        }

        public T GetResult()
        {
            return result;
        }
    }
}
