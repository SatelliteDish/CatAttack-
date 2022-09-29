using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tutorial;
using InputManagement;
using State;

public class PlayerTemp<T> : MonoBehaviour, IObservable<T>, IObserver<T>
{
    List<IObserver<T>> observers = new List<IObserver<T>>();
    Rigidbody2D myRB;
    GameManager gameManager;
    SpeedController speedController;
    TutorialScreen tutorial;
    public PlayerStates states;
    [SerializeField]PlayerConfig<T> config;
    [SerializeField]GameObject afterlife;
    [SerializeField]GameObject respawnPoint;
    [SerializeField]RespawnPlatform respawnPlatform;
    [SerializeField]AnimationManager myAnimator;
    [SerializeField]GroundDetector groundDetector;
    [SerializeField]FaceDetector faceCollider;
    float moveValue; //Done this way for performance

    void Awake() {
        SetReferences();
    }

    void SetReferences(){
    }

    void Start() {
        GetReferences();    
    }

    void GetReferences() {

    }

    private void Jump(){
        if(states.IsAlive()){
            if(groundDetector.IsTouchingGround()){
                myAnimator.IsJumping(true);
                myRB.velocity = new Vector2 (myRB.velocity.x, config.JumpHeight());
            }
        }
    }

    private void FastFall(){
        if(!groundDetector.IsTouchingGround()){
            myRB.velocity = new Vector2(myRB.velocity.x, -config.FastFallSpeed());
            if(states.IsInTutorial()){
                tutorial.SetCompletion((TutorialStates.FallComplete));
            }
        }
    }

    private void Boost(){
    if(states.IsAlive() && config.BoostCount() > 0){
        StartCoroutine(CancelBoost());    
        states.SetIsBoosting(true);
        myRB.velocity = new Vector2(config.BoostSpeed(), myRB.velocity.y);
        config.UpdateBoostCount(-1);
        gameManager.DisableBoostIndicator(config.BoostCount());
        if(states.IsInTutorial()){
            tutorial.SetCompletion((TutorialStates.BoostComplete));
            }
        }
  }

  IEnumerator CancelBoost(){
    FindObjectOfType<AudioManager>().Play("Boost");
    yield return new WaitForSecondsRealtime(config.BoostDuration());
    states.SetIsBoosting(false);
    }

    public void ManageInput(InputData input){
    switch (input.action){
        case ActionType.Jump:
            Jump();
            break;
        case ActionType.Boost:
            Boost();
            break;
        case ActionType.Fall:
            FastFall();
            break;   
        }
    }

    void Update(){
        if(!states.IsBoosting()){
            myRB.velocity = new Vector2(moveValue, myRB.velocity.y);
        }
        if(speedController.ReturnGroundSpeed().x < 0 && myRB.gravityScale > gameManager.ReturnMinGravity()){
            myRB.gravityScale = (-speedController.ReturnGroundSpeed().x/speedController.ReturnMinSpeed())* gameManager.ReturnGravityMultiplier();
        }
        else{
            myRB.gravityScale = gameManager.ReturnMinGravity();
        }
    }
    public void SetMoveValue(float val) {
        moveValue = val;
    }
    public void Die(){
        //cameraManager.SwitchToDeadCam(); Should have no access to this, but I need to remember to reimpliment it elsewhere
        //audioManager.Play("Die");
        //audioManager.Pause("Music");
        gameObject.transform.position = afterlife.transform.position;
        gameManager.ShowEndScreen(true); 
        states.SetIsAlive(false);
    }   
    public void Respawn(){
        //audioManager.Play("Music");
        states.SetHasRespawned(true);
        states.SetIsRespawning(true);
        //gameObject.gameObject.transform.position = respawnPoint.transform.position;
        //gameManager.ShowEndScreen(false);
        //cameraManager.SwitchToMainCam();
        if(respawnPlatform != null){
            return;
        }
        //var platform = Instantiate(respawnPlatform, feetCollider.transform.position, Quaternion.identity);
        //respawnPlatformScript = FindObjectOfType<RespawnPlatform>();
        states.SetCanMove(false);
        //SStartCoroutine(DestroyRespawnPlatform(platform));
    }

    IEnumerable BoostCooldown(){
        if(config.BoostCount() >= config.BoostLimit()){
            yield break;
        }
        yield return new WaitForSecondsRealtime(config.BoostCooldown());
        config.UpdateBoostCount(1);
    }

    void IObservable<T>.SubScribe(IObserver<T> observer) {
        observers.Add(observer);
    }
}
