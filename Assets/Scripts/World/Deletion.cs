using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deletion : MonoBehaviour
{
[SerializeField]Deletion self;
[SerializeField]Ground blocks;
[SerializeField]Rigidbody2D myRigidbody;
GameManager gameManager;
SpeedController speedController;
void Start(){
    GetReferences();
}
void GetReferences(){
    ManagersRepo managersRepo = FindObjectOfType<DependencyManager>().GetManagersRepo();
    gameManager = managersRepo.GetGameManager();
    speedController = managersRepo.GetSpeedController();
}
void Update(){   
   // if(!isStart){
   //     SelfDestruct(spawner.ReturnCompletionStatus());
   // }
   // else if(isStart){
   //     SelfDestruct(startSpawn.ReturnCompletionStatus());
   // }
    myRigidbody.velocity = speedController.ReturnGroundSpeed();
}
void SelfDestruct(bool status){
    if(status == true){
        Destroy(self);
    }
}
}
