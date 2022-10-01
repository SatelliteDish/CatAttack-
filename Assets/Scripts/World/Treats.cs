using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State;

public class Treats : MonoBehaviour, IDestructable {
[SerializeField]Rigidbody2D myRigidbody;
[SerializeField]BoxCollider2D myCollider;
AudioManager audioManager;
GameManager gameManager;
StateController stateController;
SpeedController speedController;
bool isBroken = false;
[SerializeField]float waitTime = 1f;
[SerializeField]GameObject sprite;
[SerializeField]GameObject treatSprite;
[SerializeField]int treatMax = 10;
[SerializeField]int treatMin = 1;
[SerializeField]Vector3 targetLocation;
[SerializeField]float speed = 5;
[SerializeField]UITweener currencyBox;
int treatsDestroyed = 0;
int rand;
void Start(){
    GetReferences();
    rand = Random.Range(treatMin, treatMax);
}
void GetReferences(){
    ManagersRepo<Treats> managersRepo = FindObjectOfType<DependencyManager<Treats>>().GetManagersRepo();
    audioManager = managersRepo.GetAudioManager();
    gameManager = managersRepo.GetGameManager();
    speedController = managersRepo.GetSpeedController();
}
private void Update(){
    myRigidbody.velocity = speedController.ReturnGroundSpeed();
}

    void IDestructable.Break(UnityEngine.GameObject destroyer) {
        if(isBroken) {
            return;
        }
        isBroken = true;
        audioManager.Play("Break");
        Destroy(sprite.gameObject);
        currencyBox.OnButtonPress("down");
        for(int i = 0; i < rand; i++){
            Instantiate(treatSprite, gameObject.transform.position, Quaternion.identity);
        }
    }
public void UpdateTreatsDestroyed(){
    treatsDestroyed++;
    if(treatsDestroyed == rand){
        Destroy(gameObject);
        currencyBox.OnButtonPress("up");
        SaveSystem.SaveCurrency(FindObjectOfType<CurrencyTracker>());
    }
}
public float ReturnWaitTime(){
    return waitTime;
}
public float ReturnSpeed(){
    return speed;
}
public Vector3 ReturnTransform(){
    return targetLocation;
}
}