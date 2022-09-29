using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    bool isTouchingGround = true;
    BoxCollider2D groundDetector;
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Ground") {
            isTouchingGround = true;
        }    
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Ground") {
            isTouchingGround = false;
        }    
    }

    public bool IsTouchingGround() {
        return isTouchingGround;
    }
}
