using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spawner;

public class BGManager : MonoBehaviour
{
[Range(0, 100)]
[SerializeField]float startBGSpeed = 10;
[SerializeField]GameObject[] deskItems;
[SerializeField]GameObject[] dresserItems;
[SerializeField]GameObject[] tableItems;
[Range(0, 100)]
[SerializeField]float bgOdds;
[SerializeField]GameObject bgParent;
float bgSpeed;
[Header("Test Info")]
[SerializeField]bool isTest = false;
void Start(){
    bgSpeed = startBGSpeed;
}
public GameObject ReturnBGParent(){
    return bgParent;
}
public float ReturnBgOdds(){
    return bgOdds;
}
public GameObject ReturnRandomBKGObject(FurniturePiece furniturePiece){
    if(furniturePiece == FurniturePiece.Desk){
        return deskItems[Random.Range(0, deskItems.Length - 1)];
    }
    if(furniturePiece == FurniturePiece.Dresser){
        return dresserItems[Random.Range(0, dresserItems.Length - 1)];
    }
    else{
        return tableItems[Random.Range(0, tableItems.Length - 1)];
    }
}
public void UpdateBGSpeed(float speed){
    bgSpeed = speed;
    if(isTest){
        Debug.Log(bgSpeed);
    }
}
}
