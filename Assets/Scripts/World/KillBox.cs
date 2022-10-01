using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
void OnTriggerEnter2D(Collider2D other) {

    if(typeof(IDestructable).IsAssignableFrom(other.gameObject.GetType())){
        IDestructable destructable = other.gameObject.GetComponent<IDestructable>();
        destructable.SelfDestruct();
    }
}
}
