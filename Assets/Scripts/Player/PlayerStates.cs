using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour {
    Player player;
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
/*************************************************************/
/*************************************************************/
    private void Start() {
        player = gameObject.GetComponent<Player>();
    }
}