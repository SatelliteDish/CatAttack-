using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spawner;

public class BGSpawner : MonoBehaviour, IDestructable, ISpawner
{
public string spawnerName {get; set;}
SpeedController speedController;
BGManager bgManager;
[SerializeField]Background background;
BoxCollider2D myCollider;
Rigidbody2D myRigidbody;
SpawnerHelper spawnerHelper;
bool hasPassedSpawn = false;
bool hasSpawned = false;
void Start(){
    GetReferences();
    spawnerName = "BGSpawner";
}
void GetReferences(){
    ManagersRepo managersRepo = FindObjectOfType<DependencyManager>().GetManagersRepo();
    bgManager = managersRepo.GetBGManager();
    speedController = managersRepo.GetSpeedController();
    spawnerHelper = managersRepo.GetSpawnerHelper();
    myCollider = GetComponent<BoxCollider2D>();
    myRigidbody = GetComponent<Rigidbody2D>();
}
void Update(){
    myRigidbody.velocity = speedController.ReturnBGSpeed();
    if(myCollider.IsTouchingLayers(LayerMask.GetMask("Spawnpoint")) && !hasPassedSpawn){
        hasPassedSpawn = true;
    }
}
public Transform GetTransform(){
    return transform;
}
public void StartSpawn(){
    if(hasSpawned){
        return;
    }
    hasSpawned = true;
    spawnerHelper.SpawnObject((ObjectType.BackDrop, bgManager.ReturnBGParent().transform, this));
}
public void SelfDestruct(){
    Destroy(background.gameObject);
}
}