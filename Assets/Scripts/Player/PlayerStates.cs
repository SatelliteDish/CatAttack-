using System;
using System.Collections.Generic;
public class PlayerStates<T>: IObservable<T> {
    List<IObserver<T>> observers = new List<IObserver<T>>();
    bool isBoosting = false;
    bool isAlive = true;
    bool canMove = true;
    bool isRespawning = false;
    bool isClothed = false;
    bool hasRespawned = false;
    bool isInTutorial = false;
    bool isTouchingGround = false;

/*************************************************************/
/*               GETTERS SETTERS AND OVERLOADS               */
/*************************************************************/
    public bool IsBoosting() {
        return isBoosting;
    }
    
    public void IsBoosting(bool state) {
        isBoosting = state;
    }
/*************************************************************/
    public bool IsAlive() {
        return isAlive;
    }

    public void IsAlive (bool state) {
        isAlive = state;
        UpdateObservers();
    }
/*************************************************************/
    public bool CanMove() {
        return canMove;
    }

    public void CanMove (bool state) {
        canMove = state;
    }
/*************************************************************/
    public bool IsRespawning() {
        return isRespawning;
    }

    public void IsRespawning (bool state) {
        isRespawning = state;
    }

/*************************************************************/
    public bool IsClothed() {
        return isClothed;
    }

    public void IsClothed (bool state) {
        isClothed = state;
    }
/*************************************************************/
    public bool HasRespawned() {
        return hasRespawned;
    }

    public void HasRespawned (bool state) {
        hasRespawned = state;
    }
/*************************************************************/
    public bool IsInTutorial() {
        return isInTutorial;
    }

    public void IsInTutorial (bool state) {
        isInTutorial = state;
    }
/*************************************************************/
    public bool IsTouchingGround() {
        return isTouchingGround;
    }

    public void IsTouchingGround(bool val) {
        isTouchingGround = val;
    }
/*************************************************************/
/*                  IOBSERVABLE METHODS                      */
/*************************************************************/
    void IObservable<T>.SubScribe(IObserver<T> observer) {
        observers.Add(observer);
    }

    void UpdateObservers() {
        foreach(var val in observers) {
            val.ObserverUpdate();
        }
    }
}