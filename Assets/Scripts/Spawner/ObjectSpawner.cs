using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spawner;

public class ObjectSpawner : MonoBehaviour, ISpawner, IDestructable
{   

public string spawnerName {get; set;}
[SerializeField]SpawnerType spawnerType;
[SerializeField]FurniturePiece furnitureType;

GameObject[] objects;
GameManager gameManager;
BGManager bgManager;
SpeedController speedController;
SpawnerHelper spawnerHelper;
Rigidbody2D myRigidbody;
BoxCollider2D myCollider;
Transform groundParent;
bool hasSpawned = false;
void Start(){
    GetReferences();
}
void GetReferences(){
    DependencyManager dependencyManager = FindObjectOfType<DependencyManager>();
    ManagersRepo managersRepo = dependencyManager.GetManagersRepo();
    WorldGenerationRepo worldGenerationRepo = dependencyManager.GetWorldGenerationRepo();
    gameManager = managersRepo.GetGameManager();
    bgManager = managersRepo.GetBGManager();
    speedController = managersRepo.GetSpeedController();
    spawnerHelper = managersRepo.GetSpawnerHelper();
    groundParent = worldGenerationRepo.GetGroundParent();
    myRigidbody = GetComponent<Rigidbody2D>();
    myCollider = GetComponent<BoxCollider2D>();
}
void Update(){
    myRigidbody.velocity = speedController.ReturnGroundSpeed();
    if(transform.position.x < 30 && !hasSpawned){
        hasSpawned = true;
        Spawn();
        SelfDestruct();
    }
}
public Transform GetTransform(){
    return transform;
}
public void SelfDestruct(){
    Destroy(gameObject);
}
public void StartSpawn(){
    
}
private SpawnerRequest BuildRequest(){
    SpawnerRequest request = new SpawnerRequest();
    request.spawner = this;
    request.type = spawnerType;
    switch (spawnerType){
        case SpawnerType.Target:
            request.spawnOBJ = ObjectType.Target;
            break;
        case SpawnerType.Hazard:
            request.spawnOBJ = ObjectType.Hazard;
            break;
        case SpawnerType.Background:
            if(furnitureType == FurniturePiece.Desk){
                request.spawnOBJ = ObjectType.DeskBG;
            }
            if(furnitureType == FurniturePiece.Dresser){
                request.spawnOBJ = ObjectType.DresserBg;
            }
            if(furnitureType == FurniturePiece.Table){
                request.spawnOBJ = ObjectType.TableBG;
            }
        break;
        case SpawnerType.Ground:
            request.spawnOBJ = ObjectType.Situation;
            request.endPiece = GetComponent<GroundSettings>().GetEndPiece();
            break;
        case SpawnerType.BackDrop:
            request.spawnOBJ = ObjectType.BackDrop;
            break;
    }  
    return request;
}

private void Spawn(){
    SpawnData spawn = spawnerHelper.GetSpawnData(BuildRequest());
    var obj = Instantiate(spawn.spawnOBJ, (Vector2)transform.position, Quaternion.identity);
    obj.transform.parent = groundParent;
    SelfDestruct();
}
}

    