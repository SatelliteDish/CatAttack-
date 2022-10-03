using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State;

public class Bounding : MonoBehaviour {
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
        gameManager = dependencyManager.GetManagersRepo().GetGameManager();
        stateController = dependencyManager.GetManagersRepo().GetStateController();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(player != null) {
            return;
        }
        if(other.tag == Constants.PlayerTag) {
            player = other.GetComponent<Player>();
        }
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
            player.SetMoveValue(-gameManager.ReturnSlowSpeed());
        }
        else{
            isAhead = false;
        }
        if(midCollider.IsTouchingLayers(LayerMask.GetMask("Player")) && !isAhead && !isBehind){
            player.SetMoveValue(0f);
        }
        if(backCollider.IsTouchingLayers(LayerMask.GetMask("Player"))){
            isBehind = true;
            player.SetMoveValue(gameManager.ReturnFastSpeed());
        }
        else{
            isBehind = false;
        }
    }
}