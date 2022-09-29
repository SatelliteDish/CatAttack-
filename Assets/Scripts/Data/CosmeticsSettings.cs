using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticsSettings : MonoBehaviour
{
[SerializeField]bool needsEars;
[SerializeField]string rarity;
public bool ReturnEarStatus(){
    return needsEars;
}
public string ReturnCosmeticRarity(){
    return rarity;
}
}
