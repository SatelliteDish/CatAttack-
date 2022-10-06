using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDestructable {
    Rigidbody2D myRigidbody;
    BoxCollider2D myCollider;
    GameManager gameManager;
    AudioManager audioManager;
    Vector2 speed = new Vector2(0,0);
    Vector2 defaultSpeed = new Vector2(0,0);
    bool isBroken = false;
    [SerializeField]int valueIndex;
    [SerializeField]float destroyTime = 1f;
    [SerializeField]ParticleSystem destroyEffect;
    [SerializeField]ParticleSystem nameAndPoint;
    [SerializeField]GameObject sprite;
    void Start() {
        GetReferences();
    }
    void GetReferences() {
        ManagersRepo managersRepo = FindObjectOfType<DependencyManager>().GetManagersRepo();
        gameManager = managersRepo.GetGameManager();
        audioManager = managersRepo.GetAudioManager();
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
    }
    void Update() {
        myRigidbody.velocity = speed;
    }
    public void Speed(Vector2 val) {
        if(speed == new Vector2(0,0)) {
            defaultSpeed = val;
        }
        speed = val;
    }

    void IDestructable.Break(GameObject obj) {
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