using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    Rigidbody2D myRigidbody;
    BoxCollider2D myCollider;
    GameManager gameManager;
    AudioManager audioManager;
    SpeedController speedController;
    StateController stateController;
    bool isBroken = false;
    [SerializeField]int valueIndex;
    [SerializeField]float destroyTime = 1f;
    [SerializeField]ParticleSystem destroyEffect;
    [SerializeField]ParticleSystem nameAndPoint;
    [SerializeField]GameObject sprite;
    void Start()
    {
        Debug.Log("Spawned " + this.transform.position);
        GetReferences();
    }
    void GetReferences(){
        ManagersRepo managersRepo = FindObjectOfType<DependencyManager>().GetManagersRepo();
        gameManager = managersRepo.GetGameManager();
        audioManager = managersRepo.GetAudioManager();
        stateController = managersRepo.GetStateController();
        speedController = managersRepo.GetSpeedController();
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
    }
    void Update(){
        if(myCollider == null | myRigidbody == null){
            Debug.LogError("Collider or Rigidbody on Target not found!");
            return;
        }
        myRigidbody.velocity = speedController.ReturnGroundSpeed();
    }
    public void Break() {
        if(isBroken){
            return;
        }
        isBroken = true;
        audioManager.Play("Break");
        gameManager.AddScore(gameManager.ReturnPoints(valueIndex));
        Destroy(sprite.gameObject);
        destroyEffect.Play();
        nameAndPoint.Play();
        StartCoroutine(SelfDestruct());
    }
    IEnumerator SelfDestruct() {
        yield return new WaitForSecondsRealtime(destroyTime);
        Destroy(gameObject);
    }
    public bool GetIsBroken() {
        return isBroken;
    }
}