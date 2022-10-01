using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHelper : MonoBehaviour {
    [SerializeField]bool isTest = false;
    void Awake(){
        FindObjectOfType<DependencyManager<TestHelper>>().SetTestHelper(this);
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
