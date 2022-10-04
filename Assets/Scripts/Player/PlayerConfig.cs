using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerConfig {
    [Range(15,35)]
    [SerializeField]float jumpHeight = 25f;
    [Range(15,35)]
    [SerializeField]float fastFallSpeed = 25f;
    [Range(1,5)]
    [SerializeField]int boostLimit = 3;
    int boostCount = 3;
    [Range(5,15)]
    [SerializeField]float boostSpeed = 10;
    [Range(0,2)]
    [SerializeField]float boostDuration = .5f;
    [Range(0,5)]
    [SerializeField]float boostCooldown = 1;
    [Range(0,10)]
    [SerializeField]float moveSpeed = 5;
    float originalSpeed = 5;

    public float JumpHeight() {
        return jumpHeight;
    }

    public float FastFallSpeed() {
        return fastFallSpeed;
    }

    public int BoostCount() {
        return boostCount;
    }
    public void BoostCount(int add) {
        boostCount += add;
    }

    public float BoostSpeed() {
        return boostSpeed;
    }

    public float BoostDuration() {
        return boostDuration;
    }

    public float BoostCooldown() {
        return boostCooldown;
    }

    public float BoostLimit() {
        return boostLimit;
    }
    public float MoveSpeed() {
        return moveSpeed;
    }
    public void MoveSpeed(float speed) {
        moveSpeed = speed;
    }
    public float OriginalSpeed() {
        return originalSpeed;
    }
    public void OriginalSpeed(float speed) {
        originalSpeed = speed;
    }
}
