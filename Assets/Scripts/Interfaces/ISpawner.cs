using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawner
{
    string spawnerName {get; set;}
    void StartSpawn();
    Transform GetTransform();
}
