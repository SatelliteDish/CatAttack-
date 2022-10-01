using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHelper : MonoBehaviour
{
[SerializeField]bool isTest = false;
void Awake(){
    FindObjectOfType<DependencyManager<T>>().SetTestHelper(this);
}
private void Start() {
    //FindObjectOfType<DependencyManager>().GetManagersRepo().GetStateController().SetState((StateType.isTest, isTest));
}
public float ReturnRandomNumber(float[] range){
    if(range.Length != 2){
        
    }
    return Random.Range(range[0], range[1]);
}
public void Log(string message){
    if(isTest){
        Debug.Log(message);
    }
}
public void LogWarning(string message){
    if(isTest){
        Debug.LogWarning(message);
    }
}
public void LogError(string message){
    if(isTest){
        Debug.LogError(message);
    }
}
}
