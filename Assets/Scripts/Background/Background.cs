using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
Rigidbody2D myRB;
SpeedController speedController;
[SerializeField]bool isStart = false;
void Start(){
    SetReferences();
}
void SetReferences(){
    ManagersRepo managersRepo = FindObjectOfType<DependencyManager>().GetManagersRepo();
    speedController = managersRepo.GetSpeedController();
    myRB = GetComponent<Rigidbody2D>();
}
void Update(){
    if(isStart){
        Move(speedController.ReturnGroundSpeed());
        return;
    }
    
    Move(speedController.ReturnBGSpeed());
}
void Move(Vector2 speed){
    myRB.velocity = speed;
}    
}
