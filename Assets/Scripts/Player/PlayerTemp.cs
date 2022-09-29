using System.Threading.Tasks.Dataflow;
using System.Transactions;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tutorial;
using InputManagement;
using State;

public class PlayerTemp<T> : MonoBehaviour, IObservable<T>
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
    [SerializeField]GameObject respawnPlatform;
    [SerializeField]AnimationManager myAnimator;
    [SerializeField]BoxCollider2D groundDetector;
    [SerializeField]FaceDetector faceCollider;
    float moveValue; //Done this way for performance

    void Awake() {
        SetReferences();
    }

    void SetReferences(){
        FindObjectOfType<DependencyManager>().SetPlayer(this);
    }

    void Start() {
        GetReferences();    
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(groundDetector.IsTouchingLayers(layerMask.GetMask(GROUND_TAG))) {
            states.IsTouchingGround(true);
        }
        if(faceCollider.IsTouchingLayers(layerMask.GetMask(HAZARD_TAG)) || faceCollider.IsTouchingLayers(layerMask.GetMask(GROUND_TAG))) {
            Die();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(groundDetector.IsTouchingLayers(layerMask.GetMask(GROUND_TAG))) {
            states.IsTouchingGround(false);
        }
    }

    void GetReferences() {
        DepedencyManager depMan = FindObjectOfType<DependencyManager>();
        gameManager = depMan.GetManagersRepo().GetGameManager();
        speedController = depMan.GetManagersRepo().GetSpeedController();
        if(states.IsInTutorial()){
            tutorial = depMan.GetUIRepo().GetTutorialScreen();
        }  
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
        states.IsBoosting(true);
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
    states.IsBoosting(false);
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
        transform.position = afterlife.transform.position;
        states.IsAlive(false);
    }   
    public void Respawn(){
        states.HasRespawned(true);
        states.IsRespawning(true);
        transform.position = respawnPoint.transform.position;
        if(respawnPlatform != null){
            return;
        }
        if(!respawnPlatform.isEnabled) {
            respawnPlatform.transform.position = new Vector2(TransformBlock.position.x, transform.position.y-1);
            respawnPlatform.isEnabled = !respawnPlatform.isEnabled;
        }
        states.CanMove(false);
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
