using System;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlatform<T> : MonoBehaviour, IObservable<T>
{
    List<IObserver<T>> observers = new List<IObserver<T>>();
    [SerializeField]BoxCollider2D groundDetector;
    [SerializeField]BoxCollider2D hazardDetector;
    [SerializeField]float platformLifetime = 3;//In Seconds

    void OnEnable() {
        Thread.Sleep((int)(platformLifetime * 1000));
        while(!CanDropPlayer()){
            Thread.Sleep(500);
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
