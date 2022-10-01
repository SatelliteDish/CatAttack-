using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
CosmeticsCatalogue cosmetics;
AnimationController animController;

private void Awake() {
    cosmetics = FindObjectOfType<DependencyManager<PlayerSpawner>>().GetCosmeticsRepo().GetCosmeticsCatalogue();
     Instantiate(cosmetics.ReturnOwnedPattern(PlayerPrefs.GetInt("Current Pattern")), new Vector3(0, 0, 0), Quaternion.identity);
}
}
