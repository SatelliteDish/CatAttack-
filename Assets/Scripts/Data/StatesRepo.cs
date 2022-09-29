using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State;
[System.Serializable]
public class StatesRepo
{
public Dictionary<StateType, bool> states { get; set;}
public void SetDictionary(Dictionary<StateType, bool> newStates){
    states = newStates;
}
public void SetStates(StateType type, bool status){
    if(states.ContainsKey(type)){
       // Debug.Log("State " + newState.type + " updated to " + newState.status + "!");
        states[type] = status;

        return;
    }
    Debug.Log("State " + type + " not found! Added, set as " + status + "!");
    states.Add(type, status);
}
public bool GetState(StateType type){
    if(!states.ContainsKey(type)){
        Debug.LogWarning("Warning: Player State " + type + " is invalid!");
        return false;
    }
    return states[type];
}
public Dictionary<StateType, bool> ReturnStatesDictionary(){
    return states;
}
}
