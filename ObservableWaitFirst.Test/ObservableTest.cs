using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace ObservableWaitFirst.Test
{
    [TestClass]
    public class ObservableTest
    {
        private class InstantlyObservable<T> : IObservable<T>
        {
            private readonly T result;

            // for test
            public BooleanDisposable Disposable { get; } = new BooleanDisposable();

            public InstantlyObservable(T result)
            {
                this.result = result;
            }

            public IDisposable Subscribe(IObserver<T> observer)
            {
                observer.OnNext(result);
                return Disposable;
            }
        }

        [TestMethod]
        public async Task TestInstantlyResult()
        {
            var observable = new InstantlyObservable<int>(1);
            int result = await observable.WaitFirst();
            Assert.AreEqual(1, result);
            Assert.AreEqual(true, observable.Disposable.IsDisposed);
        }

        private class DelayObservable<T> : IObservable<T>
        {
            private readonly T result;

            // for test
            public BooleanDisposable Disposable { get; } = new BooleanDisposable();

            public DelayObservable(T result)
            {
                this.result = result;
            }

            public IDisposable Subscribe(IObserver<T> observer)
            {
                setNext(observer);
                return Disposable;
            }

            private async void setNext(IObserver<T> observer)
            {
                await Task.Delay(1000);
                observer.OnNext(result);
            }
        }

        [TestMethod]
        public async Task TestResult()
        {
            var observable = new DelayObservable<int>(1);
            int result = await observable.WaitFirst();
            Assert.AreEqual(1, result);
            Assert.AreEqual(true, observable.Disposable.IsDisposed);
        }
    }
}
