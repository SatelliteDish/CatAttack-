using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State;

public class StateController : MonoBehaviour
{
StateUpdater stateUpdater;
Dictionary<StateType, bool> states = new Dictionary<StateType, bool>(){
                {StateType.isBoosting, false},
                {StateType.canMove, true},
                {StateType.isRespawning, false},
                {StateType.inTutorial, false},
                {StateType.inShop, false},
                {StateType.isClothed, false},
                {StateType.isAlive, true},
                {StateType.hasRespawned, false},
                {StateType.isTest, false}
};
void Awake(){
    SetReferences();
}
void SetReferences(){
    FindObjectOfType<DependencyManager>().GetManagersRepo().SetStateController(this);
}
public void SetState(StateType type, bool status){
    Debug.Log("State " + type + " changed to " + status);
    if(states.ContainsKey(type)){
        states[type] = status;
    }
    else{
        Debug.Log(type + "added and set as " + status);
        states.Add(type, status);
    }
    FindObjectOfType<DependencyManager>().GetManagersRepo().GetStateUpdater().UpdateStates(type,status);
}
public bool GetState(StateType type){
    if(states.ContainsKey(type)){
        return states[type];
    }
    else{
        Debug.LogError("Error: State " + name + " not found!");
        return false;
    }
}
}
