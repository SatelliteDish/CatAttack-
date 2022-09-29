using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticsCatalogue : MonoBehaviour
{
[SerializeField]GameObject[] accessories;
[SerializeField]Sprite[] accesoryIcon;
[SerializeField]public int[] cosmeticsOwned;
[SerializeField]int[] cosmeticsIndex;
[SerializeField]GameObject[] patterns;
[SerializeField]public Sprite[] patternIcon;
[SerializeField]public int[] patternsOwned;
[SerializeField]int[]patternsIndex;
[SerializeField]int[] commonAccessories;
[SerializeField]int[] rareAccessories;
[SerializeField]int[] epicAccessories;
[SerializeField]int[] legendaryAccessories;
public GameObject ReturnOwnedAccessory(int index){
    return accessories[cosmeticsOwned[index]];
}
public GameObject ReturnOwnedPattern(int index){
    return patterns[patternsOwned[index]];
}
public GameObject ReturnPattern(int index){
    return patterns[index];
}
public Sprite ReturnPatternIcon(int index){
    return patternIcon[index];
}
 public GameObject ReturnAccessory(int index){
    return accessories[index];
}
public Sprite ReturnAccessoryIcon(int index){
    return accesoryIcon[index];
}
public bool CheckIfOwnedAccessory(int index){
    bool isOwned = false;
    for(int i = 0; i < cosmeticsOwned.Length - 1; i++){
        if(cosmeticsOwned[i] == index){
            isOwned = true;
        }
    }
    return isOwned;
}
public bool CheckIfOwnedPattern(int index){
    bool isOwned = false;
    for(int i = 0; i < patternsOwned.Length - 1; i++){
        if(patternsOwned[i] == index){
            isOwned = true;
        }
    }
    return isOwned;
}
}