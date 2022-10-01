using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
Rigidbody2D myRigidbody;
BoxCollider2D myCollider;
GameManager gameManager;
SpeedController speedController;
int errorCount;
void Start(){
    GetReferences();
    if(myCollider != null && myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
    Destroy(gameObject);
    }
}
void GetReferences(){
    ManagersRepo<Ground> managersRepo = FindObjectOfType<DependencyManager<Ground>>().GetManagersRepo();
    gameManager = managersRepo.GetGameManager();
    speedController = managersRepo.GetSpeedController();
    if(GetComponent<Rigidbody2D>() != null){
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    if(GetComponent<BoxCollider2D>()){
    myCollider = GetComponent<BoxCollider2D>();
    }
}
void Update(){
    if(myCollider == null || myRigidbody == null){
        return;
    }
    myRigidbody.velocity = speedController.ReturnGroundSpeed();
    if(myCollider == null){
        if(errorCount >= 3){
            return;
        }
        Debug.LogError("Error: Collider not found!");
        errorCount++;
        return;
    }
    if(errorCount > 0){
        Debug.Log("Collider Found!");
    }
    if(myCollider.IsTouchingLayers(LayerMask.GetMask("End"))){
    Destroy(gameObject);
    }
}
}
