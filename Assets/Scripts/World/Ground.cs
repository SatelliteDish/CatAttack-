/********************************************************************************\
|This can be attatched to anything with RigidBody2D                              |
|Initialize on instantiation                                                     |
|Doesn't move by default, needs to be told to                                    |
|Controls the movement of the object this is attatched to, full range of movement|
\********************************************************************************/
using UnityEngine;

public class Ground : MonoBehaviour {
    Rigidbody2D myRigidbody;
    //Done this way so you know that if you see really slow it probably wasn't initialized 
    Vector2 speed = new Vector2(1f,0f);
    Vector2 defaultSpeed = new Vector2(1f,0f);
    Vector2 stopped = new Vector2(0f,0f);
    bool initialized = false;
    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    public void Initialize(Vector2 _speed) {
        if(initialized) {
            return;
        }
        defaultSpeed = _speed;
        initialized = true;
    }
    public void StopMovement() {
        speed = stopped;
    }
    public void StartMovement() {
        speed = defaultSpeed;
    }
    void Update() {
        myRigidbody.velocity = speed;
    }
}