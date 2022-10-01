using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawner : MonoBehaviour
{
[SerializeField]Ground ground;
Rigidbody2D myRigidbody;
GameManager gameManager;
SpeedController speedController;
BoxCollider2D myCollider;
bool hasPassedSpawn = false;
bool hasSpawned = false;
void Start(){
    GetReferences();
}
void GetReferences(){
    ManagersRepo managersRepo = FindObjectOfType<DependencyManager>().GetManagersRepo();
    gameManager = managersRepo.GetGameManager();
    speedController = managersRepo.GetSpeedController();
    myCollider = GetComponent<BoxCollider2D>();
    myRigidbody = GetComponent<Rigidbody2D>();
}
void Update() {
    myRigidbody.velocity = speedController.ReturnGroundSpeed();
    if(myCollider.IsTouchingLayers(LayerMask.GetMask("Spawnpoint")) && !hasPassedSpawn){
        hasPassedSpawn = true;
    }
    if(myCollider.IsTouchingLayers(LayerMask.GetMask("End"))&& hasSpawned){
        Destroy(gameObject);
    }
}
void OnTriggerEnter2D(Collider2D other){
    if(hasSpawned){
        return;
    }
    Invoke("CreateBlocks", .01f);
}
void CreateBlocks(){
    if(!hasSpawned){
        var newGrounds = Instantiate(ground, transform.position, Quaternion.identity);
        newGrounds.transform.parent = gameManager.ReturnGroundParent().transform;
        hasSpawned = true;
    }    
}
}