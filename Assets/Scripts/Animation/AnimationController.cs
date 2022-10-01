using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
    Player player;
    CosmeticsCatalogue catalogue;
    CosmeticsSettings accessory;
    DependencyManager dependencyManager;
    void Start(){
        GetReferences();
        SetRun();
    }
    void GetReferences(){
        dependencyManager = FindObjectOfType<DependencyManager>();
        WorldGenerationRepo worldGenerationRepo = dependencyManager.GetWorldGenerationRepo();
        ManagersRepo managersRepo = dependencyManager.GetManagersRepo();
        CosmeticsRepo cosmeticsRepo = dependencyManager.GetCosmeticsRepo();
        catalogue = cosmeticsRepo.GetCosmeticsCatalogue();
        player = worldGenerationRepo.GetPlayer();
    }
    public void SetRun(){
        if(player == null){
            return;
        }
        player.gameObject.GetComponent<Animator>().Play("Run", -1, 0);
        if(accessory != null){
            accessory.gameObject.GetComponent<Animator>().Play("Run", -1, 0);
        }
    }
    public void SetInAir(bool inAir){
        if(player == null){
            return;
        }
        player.gameObject.GetComponent<Animator>().SetBool("inAir", inAir);
        if(accessory != null){
            accessory.gameObject.GetComponent<Animator>().SetBool("inAir", inAir);
        }
    }
    public void ResetPlayerVariable(){
        player = FindObjectOfType<DependencyManager>().GetWorldGenerationRepo().GetPlayer();
    }
}