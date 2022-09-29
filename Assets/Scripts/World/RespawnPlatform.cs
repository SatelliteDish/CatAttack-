using System;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlatform<T> : MonoBehaviour, IObservable
{
    List<IObserver<T>> observers = new List<IObserver<T>>();
    [SerializeField]BoxCollider2D groundDetector;
    [SerializeField]BoxCollider2D hazardDetector;
    [SerializaField]float platformLifetime = 3;//In Seconds

    void OnEnable() {
        Thread.Sleep(platformLifetime * 1000);
        if(!CanDropPlayer()){
            Thread.Sleep(500);
            continue;
        }
        if(this.enabled) {
            this.enabled = !this.enabled;
        }
    }

    bool CanDropPlayer() {
        if(groundDetector.IsTouchingLayers(LayerMask.GetMask("Ground")) | !hazardDetector.IsTouchingLayers(LayerMask.GetMask("Hazard"))){
            return true;
        }
        return false;
    }

    void IObservable<T>.SubScribe(IObserver<T> observer) {
            observers.Add(observer);
    }
}
