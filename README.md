## ObservableWaitFirst
**This repository is experimental source code**

### Description
*WaitFirst* extension method is await first *OnNext* event for `IObservable<T>`.

```csharp
// observable: IObservable<T>
var result = await observable.WaitFirst();
```

*WaitFirst* is using Awaitable-pattern, not using Task-Like pattern.

see also ObservableWaitFirst project.