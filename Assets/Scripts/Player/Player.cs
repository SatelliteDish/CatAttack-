using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using InputManagement;
using Tutorial;
using State;

public class Player : MonoBehaviour
{
[SerializeField]StatesRepo states;
Dictionary<StateType,bool> statesDict = new Dictionary<StateType,bool>(){
                {StateType.isBoosting, false},
                {StateType.canMove, true},
                {StateType.isRespawning, false},
                {StateType.inTutorial, false},
                {StateType.inShop, false},
                {StateType.isClothed, false},
                {StateType.isAlive, true},
};
Rigidbody2D myRigidbody;
StateController stateController;
GameManager gameManager;
[SerializeField]BoxCollider2D feetCollider;
[SerializeField]BoxCollider2D myCollider;
[SerializeField]BoxCollider2D groundDetection;
[SerializeField]BoxCollider2D faceBox;
RespawnPlatform respawnPlatformScript;
[SerializeField]GameObject afterLife;
[SerializeField]GameObject respawnPoint;
[SerializeField]GameObject respawnPlatform;
[SerializeField]float jumpHeight = 5f;
[SerializeField]float fastFallSpeed = 2f;
[SerializeField]float boostTime = .5f;
[SerializeField]float boostSpeed = 20f;
[SerializeField]float boostCooldown = 2f;
float moveValue;
float currentspeed;
Animator myAnimator;
[SerializeField]int boostCount;
TutorialScreen tutorialScreen;
[SerializeField]Animator cosmetic;
[SerializeField]ControlManager controlManager;
CosmeticsCatalogue catalogue;
CosmeticsScreen cosmeticsScreen;
StateUpdater stateUpdater;
CosmeticsSettings settings;
AnimationController animController;
SpeedController speedController;
AudioManager audioManager;
CameraManager cameraManager;
void Awake(){
    SetReferences();
    //SpawnCosmetic();
}
void Start(){
    GetReferences();
    animController.ResetPlayerVariable();
    animController.SetRun();
    if(cosmeticsScreen != null){
        SetState(StateType.inShop, true);
    }
    if(tutorialScreen != null){
        //:211SetState((StateType.inTutorial, true));
    }
    boostCount = gameManager.ReturnBoostCount();
}
void SetState(StateType type, bool status){
    stateController.SetState(type,status);
}
void SetReferences(){
    DependencyManager dependencyManager = FindObjectOfType<DependencyManager>();
    dependencyManager.GetWorldGenerationRepo().SetPlayer(this);
    dependencyManager.GetManagersRepo().SetControlManager(controlManager);
    dependencyManager.GetManagersRepo().GetStateUpdater().SetStateRepo(("player", states));
    states.SetDictionary(statesDict);
}
void GetReferences(){
    DependencyManager dependencyManager = FindObjectOfType<DependencyManager>();
    WorldGenerationRepo worldGenerationRepo = dependencyManager.GetWorldGenerationRepo();
    ManagersRepo managersRepo = dependencyManager.GetManagersRepo();
    CosmeticsRepo cosmeticsRepo = dependencyManager.GetCosmeticsRepo();
    UIRepo uIRepo = dependencyManager.GetUIRepo();
    stateUpdater = dependencyManager.GetManagersRepo().GetStateUpdater();
    stateController = dependencyManager.GetManagersRepo().GetStateController();
    animController = managersRepo.GetAnimationController();
    audioManager = managersRepo.GetAudioManager();
    cameraManager = managersRepo.GetCameraManager();
    catalogue = cosmeticsRepo.GetCosmeticsCatalogue();
    gameManager = managersRepo.GetGameManager();
    tutorialScreen = uIRepo.GetTutorialScreen();
    cosmeticsScreen = cosmeticsRepo.GetCosmeticsScreen();
    speedController = managersRepo.GetSpeedController();
    afterLife = worldGenerationRepo.GetAfterlife();
    myAnimator = GetComponent<Animator>();
    myRigidbody = GetComponent<Rigidbody2D>();
}
public void SpawnCosmetic(){//unimplemented
    var accessory= Instantiate(catalogue.ReturnOwnedAccessory(PlayerPrefs.GetInt("Current Accessory")), new Vector3 (0, 0, 0), Quaternion.identity);
    accessory.transform.parent = this.transform;
    if(PlayerPrefs.GetInt("Current Accessory") != 0){
        SetState(StateType.isClothed, true);
        cosmetic = GetComponentInChildren<Animator>();
        settings = FindObjectOfType<CosmeticsSettings>();
        if(settings.ReturnEarStatus() != true){
            myAnimator.SetTrigger("isClothed");
        }
    }
}
void Update(){
    CheckState();
    if(!states.GetState(StateType.isBoosting)){
        myRigidbody.velocity = new Vector2(moveValue, myRigidbody.velocity.y);
    }
    if(myCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")) && !states.GetState(StateType.isRespawning)){
        Die();
    }
    if(speedController.ReturnGroundSpeed().x < 0 && myRigidbody.gravityScale > gameManager.ReturnMinGravity()){
        myRigidbody.gravityScale = (-speedController.ReturnGroundSpeed().x/speedController.ReturnMinSpeed())* gameManager.ReturnGravityMultiplier();
    }
    else{
        myRigidbody.gravityScale = gameManager.ReturnMinGravity();
    }
}
void CheckState(){
    if(!states.GetState(StateType.inShop) && !states.GetState(StateType.isRespawning)){
        animController.SetInAir(!groundDetection.IsTouchingLayers(LayerMask.GetMask("Ground")));
    }
    else if(states.GetState(StateType.inShop)){
        animController.SetInAir(false);
        if(states.GetState(StateType.isClothed)){
            cosmetic.SetBool("inAir", false);
        }
    }
    else if(states.GetState(StateType.isRespawning)){
        animController.SetInAir(true);
        if(states.GetState(StateType.isClothed)){
            cosmetic.SetBool("inAir", false);
        }
    }
}
private void FastFall(){
    if(!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, -fastFallSpeed);
        if(states.GetState(StateType.inTutorial)){
            tutorialScreen.SetCompletion((TutorialStates.FallComplete));
        }
    }
}
void OnTriggerEnter2D(Collider2D other) {
    if(stateController.GetState(StateType.isRespawning)){
        return;
    }
    if(faceBox.IsTouchingLayers(LayerMask.GetMask("Ground")) && other.tag == "Ground" | other.tag == "Hazard"){
        Die();
    }
    if(other.tag == "Target"){
        other.gameObject.GetComponent<Target>().Break();    
    }
}
public void Die(){
    currentspeed = speedController.ReturnGroundSpeed().x;
    cameraManager.SwitchToDeadCam();
    audioManager.Play("Die");
    audioManager.Pause("Music");
    gameObject.transform.position = afterLife.transform.position;
    gameManager.ShowEndScreen(true); 
    stateController.SetState(StateType.isAlive, false);
}
public void Respawn(){
    audioManager.Play("Music");
    stateController.SetState(StateType.hasRespawned, true);
    stateController.SetState(StateType.isRespawning, true);
    gameObject.gameObject.transform.position = respawnPoint.transform.position;
    gameManager.ShowEndScreen(false);
    cameraManager.SwitchToMainCam();
    if(respawnPlatformScript != null){
        return;
    }
    var platform = Instantiate(respawnPlatform, feetCollider.transform.position, Quaternion.identity);
    respawnPlatformScript = FindObjectOfType<RespawnPlatform>();
    stateController.SetState(StateType.canMove, false);
    StartCoroutine(DestroyRespawnPlatform(platform));
}
IEnumerator DestroyRespawnPlatform(GameObject platform){ //put in respawn platform
    stateController.SetState(StateType.isAlive, true);
    yield return new WaitForSecondsRealtime(gameManager.ReturnRespawnTime());
    stateController.SetState(StateType.isRespawning, false);
    respawnPlatformScript.SelfDestruct();
    stateController.SetState(StateType.canMove, true);
    boostCount = gameManager.ReturnBoostCount();
}
private void Jump(){
    if(!states.GetState(StateType.isAlive)){
        return;
    }
    if(feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
        myAnimator.SetBool("isJumping", true);
        if(states.GetState(StateType.isClothed)){
            cosmetic.SetBool("isJumping", true);
        }
        myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpHeight);
        if(states.GetState(StateType.inTutorial)){
            tutorialScreen.SetCompletion((TutorialStates.JumpComplete));
        }
    }
}
private void Boost()
{
    if(states.GetState(StateType.isAlive) && boostCount > 0){
        StartCoroutine(CancelBoost());    
        stateController.SetState(StateType.isBoosting, true);
        myRigidbody.velocity = new Vector2(boostSpeed, myRigidbody.velocity.y);
        --boostCount;
        gameManager.DisableBoostIndicator(boostCount);
        if(states.GetState(StateType.inTutorial)){
            tutorialScreen.SetCompletion((TutorialStates.BoostComplete));
            }
        }
  }
IEnumerator CancelBoost(){
    FindObjectOfType<AudioManager>().Play("Boost");
    yield return new WaitForSecondsRealtime(boostTime);
    stateController.SetState(StateType.isBoosting, false);
}
public void StartBoostCooldown(){
    if(gameObject !=null){
        StartCoroutine(ManageBoostCooldown());
    }
}
IEnumerator ManageBoostCooldown()
{
    if(boostCount < gameManager.ReturnBoostCount()){
        yield return new WaitForSecondsRealtime(boostCooldown);
        if(boostCount<gameManager.ReturnBoostCount()){
            ++boostCount;
        }
    }
}
public void UpdatesSpeedValue(float value){
moveValue = value;
}
public int ReturnBoostAbility(){
    return boostCount ;
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
}
