using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tutorial;
using State;
public class Coins : MonoBehaviour, IDestructable {
    GameManager gameManager;
    [SerializeField]int valueIndex;
    AudioManager audioManager;
    DependencyManager dependencyManager;
    bool isCollected = false;
    void Start(){
        GetReferences();
    }
    void GetReferences(){
        dependencyManager = FindObjectOfType<DependencyManager>();
        ManagersRepo managersRepo = dependencyManager.GetManagersRepo();
        gameManager = managersRepo.GetGameManager();
        audioManager = managersRepo.GetAudioManager();
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    void IDestructable.Break(GameObject destroyer) {
        if(isCollected) {
            return;
        }
        try {
            isCollected = true;
            audioManager.Play("Coin");
            gameManager.AddScore(gameManager.ReturnPoints(valueIndex));
            Destroy(gameObject);
        }
        catch(ArgumentException error) {
            Debug.LogError(error.Message);
        }
    }
}