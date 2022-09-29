using System.Collections.Generic;

public interface IObservable<in T>
{
    void SubScribe(IObserver<T> observer);
}
