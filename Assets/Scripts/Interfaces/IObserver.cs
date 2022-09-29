using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver<out T>
{
    public void ObserverUpdate();
}
