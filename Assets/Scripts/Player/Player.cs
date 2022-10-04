using System.Threading;
using System.Transactions;
using System.Collections;
using UnityEngine;
using Tutorial;
using InputManagement;

public class Player : MonoBehaviour {
    Rigidbody2D myRB;
    AnimationManager myAnimator;
    [SerializeField]GameManager gameManager;
    [SerializeField]SpeedController speedController;
    [SerializeField]TutorialScreen tutorial;
    public PlayerStates states;
    public PlayerConfig config;
    [SerializeField]GameObject afterlife;
    [SerializeField]GameObject respawnPoint;
    [SerializeField]GameObject respawnPlatform;
    [SerializeField]BoxCollider2D groundDetector;
    [SerializeField]BoxCollider2D faceCollider;
    float moveValue; //Done this way for performance
    void Start() {
        myRB = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<AnimationManager>();
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(groundDetector.IsTouchingLayers(LayerMask.GetMask(Constants.GroundTag))) {
            states.IsTouchingGround(true);
        }
        if(faceCollider.IsTouchingLayers(LayerMask.GetMask(Constants.HazardTag)) || faceCollider.IsTouchingLayers(LayerMask.GetMask(Constants.GroundTag))) {
            Die();
        }
        if(typeof(IDestructable).IsAssignableFrom(other.gameObject.GetType())) {
            IDestructable destructable = other.gameObject.GetComponent<IDestructable>();
            destructable.Break(this.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(groundDetector.IsTouchingLayers(LayerMask.GetMask(Constants.GroundTag))) {
            states.IsTouchingGround(false);
        }
    }

    private void Jump(){
        if(states.IsAlive()){
            if(groundDetector.IsTouchingLayers(LayerMask.GetMask(Constants.GroundTag))){
                myAnimator.IsJumping(true);
                myRB.velocity = new Vector2 (myRB.velocity.x, config.JumpHeight());
            }
        }
    }

    private void FastFall(){
        if(!groundDetector.IsTouchingLayers(LayerMask.GetMask(Constants.GroundTag))){
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
        gameManager.PlayerDeath(true);
        transform.position = afterlife.transform.position;
        states.IsAlive(false);
    }   
    public void Respawn(){
        gameManager.PlayerDeath(false);
        states.HasRespawned(true);
        states.IsRespawning(true);
        transform.position = respawnPoint.transform.position;
        if(respawnPlatform != null){
            return;
        }
        if(!respawnPlatform.activeInHierarchy) {
            respawnPlatform.transform.position = new Vector2(transform.position.x, transform.position.y-1);
            respawnPlatform.SetActive(true);
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
}
