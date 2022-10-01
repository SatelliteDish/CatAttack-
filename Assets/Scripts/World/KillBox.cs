using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
void OnTriggerEnter2D(Collider2D other) {

    if(typeof(ITemp).IsAssignableFrom(other.gameObject.GetType())){
        ITemp destructable = other.gameObject.GetComponent<ITemp>();
        destructable.SelfDestruct();
    }
}
}
