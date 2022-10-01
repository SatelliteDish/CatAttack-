using System.Collections.Generic;
using UnityEngine;
using State;

public class StateUpdater : MonoBehaviour
{
[SerializeField]string[] stateRepoNames;
Dictionary<string, StatesRepo> stateRepos = new Dictionary<string, StatesRepo>(){

};
void Awake() {
    SetReferences();    
}
void SetReferences(){
    FindObjectOfType<DependencyManager<StateUpdater>>().GetManagersRepo().SetStateUpdater(this);
}
public void SetStateRepo((string name, StatesRepo states) newRepo){
    int length = stateRepoNames.Length;
    stateRepos.Add(newRepo.name, newRepo.states);
    stateRepoNames = new string[length + 1];
    stateRepoNames[length] = newRepo.name;
}
public void UpdateStates(StateType type, bool status){
    for(int i = 0; i < stateRepoNames.Length; i++){
        if(stateRepos[stateRepoNames[i]].ReturnStatesDictionary().ContainsKey(type)){
            stateRepos[stateRepoNames[i]].SetStates(type, status);
        }
    }
}
}
