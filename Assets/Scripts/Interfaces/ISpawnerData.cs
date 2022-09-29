using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnerData
{
GameObject[] items {get; set;}
GameObject[] GetAllItems();
GameObject GetItem(int index);
GameObject GetRandomItem();
void SetItems();
}
