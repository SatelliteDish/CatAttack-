using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDetector : MonoBehaviour
{
    Player player;
    void Start() {
        player = GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Hazard") {
            player.Die();
        }
    }
}
