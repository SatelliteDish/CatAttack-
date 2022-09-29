using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlatform : MonoBehaviour
{
bool canDropPlayer = false;
[SerializeField]BoxCollider2D groundDetector;
[SerializeField]BoxCollider2D hazardDetector;
void Update(){
    if(!groundDetector.IsTouchingLayers(LayerMask.GetMask("Ground")) | hazardDetector.IsTouchingLayers(LayerMask.GetMask("Hazard"))){
        canDropPlayer = false;
    }
    else{
        canDropPlayer = true;
    }
}
public void SelfDestruct(){
    if(canDropPlayer){
        Destroy(gameObject);
    }
    else{
        Invoke("SelfDestruct", .1f);
    }
}
}
