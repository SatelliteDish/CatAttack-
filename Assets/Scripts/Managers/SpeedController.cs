using UnityEngine;
using State;
public class SpeedController : MonoBehaviour
{
[SerializeField]float minSpeed;
[SerializeField]float maxSpeed;
float groundSpeed;
float bgSpeed;
float gameSpeed;
[Range(0,1)]
[SerializeField]float bgSpeedPercent;
[Range(0,1)]
[SerializeField]float groundSpeedPercent;
[Range(0,10)]
[SerializeField]float timeMultiplier;
GameManager gameManager;
StateController stateController;
void Start() {
    SetReferences();
}
void SetReferences(){
    ManagersRepo<SpeedController> managersRepo = FindObjectOfType<DependencyManager<SpeedController>>().GetManagersRepo();
    gameManager = managersRepo.GetGameManager();
    stateController = managersRepo.GetStateController();
}
void Update(){
    if(!stateController.GetState(StateType.isAlive)){
        Move(0);
        return;
    }
    if(gameSpeed > maxSpeed){
        Move(maxSpeed);
        return;
    }
    AddSpeed();
    if(gameSpeed < minSpeed){
        Move(minSpeed);
    }
    else if(groundSpeed >= minSpeed && groundSpeed <= maxSpeed){
        Move(gameSpeed);
    }
    else{
        Move(maxSpeed);
    }
}
public float ReturnMinSpeed(){
    return minSpeed;
}
void Move(float speed){
    groundSpeed = speed * groundSpeedPercent;
    bgSpeed = speed * bgSpeedPercent;
}
void AddSpeed(){
    gameSpeed = gameSpeed + (Time.deltaTime/timeMultiplier);
} 
public Vector2 ReturnGroundSpeed(){
    return new Vector2(-groundSpeed, 0);
}
public Vector2 ReturnBGSpeed(){
    return new Vector2(-bgSpeed, 0);
}
}
