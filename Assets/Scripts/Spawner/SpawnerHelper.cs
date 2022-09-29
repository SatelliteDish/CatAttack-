using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spawner;

public class SpawnerHelper : MonoBehaviour
{
Dictionary<ObjectType, GameObject[]> spawnerData = new Dictionary<ObjectType, GameObject[]>(){
};
GameManager gameManager;
[SerializeField]GameObject[] backDrops;
[SerializeField]GameObject[] tableBGObjects;
[SerializeField]GameObject[] dresserBGObjects;
[SerializeField]GameObject[] deskBGObjects;
[SerializeField]GameObject[] hazards;
[SerializeField]GameObject[] targets;
[SerializeField]GameObject[] situations;
GameObject lastBackdrop;
void Awake(){
    spawnerData.Add(ObjectType.BackDrop, backDrops);
    spawnerData.Add(ObjectType.TableBG, tableBGObjects);
    spawnerData.Add(ObjectType.DeskBG, deskBGObjects);
    spawnerData.Add(ObjectType.DresserBg, dresserBGObjects);
    spawnerData.Add(ObjectType.Hazard, hazards);
    spawnerData.Add(ObjectType.Target, targets);
}
void Start(){
    GetReferences();
}
void GetReferences(){
    gameManager = FindObjectOfType<DependencyManager>().GetManagersRepo().GetGameManager();
}
public void SpawnObject((ObjectType type, Transform parent, ISpawner spawner) newObject){
    //Debug.Log("Spawning " + newObject.type + " from " + newObject.spawner.spawnerName);
    Vector2 position = new Vector2(newObject.spawner.GetTransform().position.x, newObject.spawner.GetTransform().position.y);
    if(newObject.type == ObjectType.BackDrop){
        position = new Vector2(newObject.spawner.GetTransform().position.x - .5f, 0);
        GameObject newBackdrop = backDrops[Random.Range(0, backDrops.Length)];
        while(newBackdrop == newObject.parent.gameObject){
            newBackdrop = backDrops[Random.Range(0, backDrops.Length)];
        }
        var _backdrop = Instantiate(newBackdrop, position, Quaternion.identity);
            _backdrop.transform.SetParent(newObject.parent);
            Debug.Log("Successfully spawned " + newObject.type);
            return;
    }
    if(newObject.type == ObjectType.Target){
        SpawnTarget((newObject.parent, newObject.spawner));
        return;
    }
    GameObject[] items = spawnerData[newObject.type];
    var _object = Instantiate(items[Random.Range(0, items.Length)], position, Quaternion.identity);
    _object.transform.SetParent(newObject.parent);
    Debug.Log("Successfully spawned " + newObject.type);
}
void SpawnTarget((Transform parent, ISpawner spawner) data){
    int chance = Random.Range(0, 101);
    float currentPercent = 0;
    for(int i = 0; currentPercent < chance; i++){
        currentPercent = currentPercent + gameManager.ReturnOdds(i + 1);
        if(currentPercent >= chance){
            var _object = Instantiate(targets[i + 1], data.spawner.GetTransform().position, Quaternion.identity);
            _object.transform.parent = data.parent;
            return;
        }
    }
}
public SpawnData GetSpawnData(SpawnerRequest request){
    SpawnData data = new SpawnData();
    data.spawnOBJ = targets[1];
    switch(request.spawnOBJ){
        case ObjectType.Target:
            break;
    
        case ObjectType.Hazard:
            data.spawnOBJ = hazards[Random.Range(0, hazards.Length)];
            break;
    
        case ObjectType.Situation:
            data.spawnOBJ = GetRandomSituation(request);
            break;
        case ObjectType.TableBG:
            data.spawnOBJ = tableBGObjects[Random.Range(0, tableBGObjects.Length)];
        break;
        case ObjectType.DeskBG:
            data.spawnOBJ = deskBGObjects[Random.Range(0, deskBGObjects.Length)];
        break;
        case ObjectType.DresserBg:
            data.spawnOBJ = dresserBGObjects[Random.Range(0, dresserBGObjects.Length)];
        break;
        case ObjectType.BackDrop:
            data.spawnOBJ = GetRandomBackdrop();
            break;
    }
    return data;
}
    private GameObject GetRandomBackdrop(){
        GameObject result = backDrops[Random.Range(0,backDrops.Length)];
        while(result == lastBackdrop){
            result = backDrops[Random.Range(0,backDrops.Length)];
        }
        lastBackdrop = result;
        return result;
    }

    private GameObject GetRandomSituation(SpawnerRequest request){
        GameObject result = situations[Random.Range(0,situations.Length - 1)];
        while(request.endPiece == result.GetComponentInChildren<GroundSettings>().GetStartPiece()){
            result = situations[Random.Range(0, situations.Length - 1)];
        }

        return result;
    }
}
