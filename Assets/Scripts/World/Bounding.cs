using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State;

public class Bounding : MonoBehaviour
{
[SerializeField]BoxCollider2D frontCollider;
[SerializeField]BoxCollider2D midCollider;
[SerializeField]BoxCollider2D backCollider;
Player player;
DependencyManager dependencyManager;
StateController stateController;
GameManager gameManager;
bool isAhead = false;
bool isBehind = false;
void Start(){
    GetReferences();
}
void GetReferences(){
    dependencyManager = FindObjectOfType<DependencyManager>();
    player = dependencyManager.GetWorldGenerationRepo().GetPlayer();
    gameManager = dependencyManager.GetManagersRepo().GetGameManager();
    stateController = dependencyManager.GetManagersRepo().GetStateController();
}
void Update(){
    if(stateController.GetState(StateType.isBoosting)){
        return;
    }
    MaintainPosition();
}
void MaintainPosition(){
    if(frontCollider.IsTouchingLayers(LayerMask.GetMask("Player"))){
        isAhead = true;
        player.UpdatesSpeedValue(-gameManager.ReturnSlowSpeed());
    }
    else{
        isAhead = false;
    }
    if(midCollider.IsTouchingLayers(LayerMask.GetMask("Player")) && !isAhead && !isBehind){
        player.UpdatesSpeedValue(0f);
        player.StartBoostCooldown();
    }
    if(backCollider.IsTouchingLayers(LayerMask.GetMask("Player"))){
        isBehind = true;
        player.UpdatesSpeedValue(gameManager.ReturnFastSpeed());
        player.StartBoostCooldown();
    }
    else{
        isBehind = false;
    }
}
}
