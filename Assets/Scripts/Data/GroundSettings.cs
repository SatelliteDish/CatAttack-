using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spawner;
public class GroundSettings : MonoBehaviour
{
[SerializeField]FurniturePiece startPiece;
[SerializeField]FurniturePiece endPiece;
public FurniturePiece GetStartPiece(){
    return startPiece;
}
public FurniturePiece GetEndPiece(){
    return endPiece;
}
public void SelfDestruct(){
    Destroy(gameObject);
}
}
