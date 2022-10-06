using System.Threading;
using System;
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
    [SerializeField]AudioManager audioManager;
    public PlayerStates states;
    public PlayerConfig config;
    [SerializeField]GameObject afterlife;
    [SerializeField]GameObject respawnPoint;
    [SerializeField]GameObject respawnPlatform;
    [SerializeField]BoxCollider2D groundDetector;
    [SerializeField]BoxCollider2D faceCollider;
    void Start() {
        myRB = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<AnimationManager>();
        audioManager = FindObjectOfType<AudioManager>();
        config.OriginalSpeed(config.MoveSpeed());
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(faceCollider.IsTouchingLayers(LayerMask.GetMask(Constants.HazardTag)) || faceCollider.IsTouchingLayers(LayerMask.GetMask(Constants.GroundTag))) {
            Die();
        }
        if(typeof(IDestructable).IsAssignableFrom(other.gameObject.GetType())) {
            IDestructable destructable = other.gameObject.GetComponent<IDestructable>();
            destructable.Break(this.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D other) {
        if(groundDetector.IsTouchingLayers(LayerMask.GetMask(Constants.GroundTag))) {
            states.IsTouchingGround(true);
        }    
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(!groundDetector.IsTouchingLayers(LayerMask.GetMask(Constants.GroundTag))) {
            states.IsTouchingGround(false);
        }
    }

    private void Jump(){
        if(!states.IsAlive() || !states.IsTouchingGround()){
            return;
        }
        myAnimator.IsJumping(true);
        myRB.velocity = new Vector2 (myRB.velocity.x, config.JumpHeight());
    }

    private void FastFall(){
        if(states.IsTouchingGround()) {
            return;
        }
        myRB.velocity = new Vector2(myRB.velocity.x, -config.FastFallSpeed());
        if(states.IsInTutorial()){
            tutorial.SetCompletion((TutorialStates.FallComplete));
        }
    }

    private void Boost() {
        if(!states.IsAlive() || config.BoostCount() < 1 || states.IsBoosting()) {
            return;
        }
        try {
            states.IsBoosting(true);
            audioManager.Play("Boost");
            Debug.Log("Boost Speed = " + config.BoostSpeed());
            config.MoveSpeed(config.BoostSpeed());
            config.BoostCount(-1);
            gameManager.DisableBoostIndicator(config.BoostCount());
            StartCoroutine(EndBoost());
        }
        catch(ArgumentException error) {
            Debug.LogError(error.Message);
        }
    }
    IEnumerator EndBoost() {
        yield return new WaitForSecondsRealtime(config.BoostCooldown());
        Thread.Sleep((int)(config.BoostDuration() * 100));
        config.MoveSpeed(config.OriginalSpeed());
        Debug.Log("Move Speed = " + config.MoveSpeed());
        states.IsBoosting(false);
        StartCoroutine(ReplenishBoost());
    }
    IEnumerator ReplenishBoost() {
        yield return new WaitForSecondsRealtime(config.BoostCooldown());
        config.BoostCount(config.BoostCount() + 1);
    }

    public void ManageInput(InputData input) {
    switch (input.action) {
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

    void Update() {
        if(speedController.ReturnGroundSpeed().x < 0 && myRB.gravityScale > gameManager.ReturnMinGravity()) {
            myRB.gravityScale = (-speedController.ReturnGroundSpeed().x/speedController.ReturnMinSpeed())* gameManager.ReturnGravityMultiplier();
        }
        else {
            myRB.gravityScale = gameManager.ReturnMinGravity();
        }
        if(!(transform.position.x < .1 && transform.position.x > -.1) || states.IsBoosting()) {
            if(transform.position.x < 0 || states.IsBoosting()) {
                myRB.velocity = new Vector2(config.MoveSpeed(), myRB.velocity.y);
            }
            else if(transform.position.x > 0) {
                myRB.velocity = new Vector2(-config.MoveSpeed(), myRB.velocity.y);
            }
        }
    }
    public void Die() {
        states.IsAlive(false);
        gameManager.PlayerDeath(true);
        transform.position = afterlife.transform.position;
    }   
    public void Respawn() {
        if(respawnPlatform != null) {
            return;
        }
        states.HasRespawned(true);
        states.IsRespawning(true);
        gameManager.PlayerDeath(false);
        transform.position = respawnPoint.transform.position;
        if(!respawnPlatform.activeInHierarchy) {
            respawnPlatform.transform.position = new Vector2(transform.position.x, transform.position.y-1);
            respawnPlatform.SetActive(true);
        }
        states.CanMove(false);
    }
}
