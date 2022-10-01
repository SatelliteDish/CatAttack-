using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tutorial;
using State;
public class Coins : MonoBehaviour
{
Rigidbody2D myRigidbody;
GameManager gameManager;
[SerializeField]int valueIndex;
AudioManager audioManager;
DependencyManager<T> dependencyManager;
bool isCollected = false;
TutorialScreen tutorialScreen;
SpeedController speedController;
StateController stateController;
void Start(){
    GetReferences();
}
void GetReferences(){
    dependencyManager = FindObjectOfType<DependencyManager<T>>();
    ManagersRepo<T> managersRepo = dependencyManager.GetManagersRepo();
    stateController = managersRepo.GetStateController();
    speedController = managersRepo.GetSpeedController();
    gameManager = managersRepo.GetGameManager();
    audioManager = managersRepo.GetAudioManager();
    tutorialScreen = dependencyManager.GetUIRepo().GetTutorialScreen();
    myRigidbody = GetComponent<Rigidbody2D>();
}
void Update(){
    myRigidbody.velocity = speedController.ReturnGroundSpeed();
}
void OnTriggerEnter2D(Collider2D other){
    if(other.tag == "End"){
        Destroy(gameObject);
        if(stateController.GetState(StateType.inTutorial)){
            tutorialScreen.SetCompletion(TutorialStates.CoinMissed);
        }
    }
    if(other.tag == "Player"&& !stateController.GetState(StateType.isRespawning) && !isCollected){
        isCollected = true;
        audioManager.Play("Coin");
        gameManager.AddScore(gameManager.ReturnPoints(valueIndex));
        Destroy(gameObject);
        if(stateController.GetState((StateType.inTutorial))){
            tutorialScreen.SetCompletion(TutorialStates.CoinCollected);
        }
    }
}
}