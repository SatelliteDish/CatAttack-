using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WorldGenerationRepo
{
[SerializeField]Bounding bounding;
[SerializeField]Deletion deletion;
[SerializeField]Ground ground;
[SerializeField]Player player;
[SerializeField]RespawnPlatform respawnPlatform;
[SerializeField]ObjectSpawner targetSpawner;
[SerializeField]GameObject afterlife;
[SerializeField]Transform groundParent;
[SerializeField]Transform bgParent;
[SerializeField]TutorialScreen tutorialScreen;
TestHelper testHelper;
public Transform GetGroundParent(){
    return groundParent;
}
public Transform GetBGParent(){
    return bgParent;
}
public GameObject GetAfterlife(){
    return afterlife;
}
public void SetTestHelper(TestHelper helper){
    testHelper = helper;
}
public void SetPlayer(Player _player){
    if(_player == null){
        testHelper.LogError("Error: Reference to Player not found!");
        return;
    }
    player = _player;
}
public Player GetPlayer(){
    if(player == null){
        testHelper.LogError("Error: Player not found!");
        return null;
    }
    return player;
}
}
