using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticPicker : MonoBehaviour{
    ShopManager shopManager;
    CosmeticsCatalogue catalogue;
    DependencyManager dependencyManager;
    bool isBasic;
    bool isAccessory;
    int cosmeticWon;
    int packageSelected;
    [SerializeField]float[] rarities = new float[4];
    void Start(){
        GetReferences();
    }
    void GetReferences(){
        dependencyManager = FindObjectOfType<DependencyManager>();
        catalogue = dependencyManager.GetCosmeticsRepo().GetCosmeticsCatalogue();
        shopManager = dependencyManager.GetCosmeticsRepo().GetShopManager();
    }
    public void PickCosmetic(){
        int rand = Random.Range(0, 101);
        Debug.Log("Starting Pick Process " + rand);
        if(packageSelected == 0){
            GetBasicRarity(rand);
        }
        if(packageSelected == 1){
            GetDeluxeRarity(rand);
        }
         if(packageSelected == 2){
            GetProRarity(rand);
        }   
    }

    void GetBasicRarity(int rand){
        for(int currentRarity = 0; currentRarity < rarities.Length; currentRarity++){
            float actualRarity = 0;
            for(int i = 1; i < currentRarity + 1; i++){
                Debug.Log(i);
                if(shopManager != null){
                    Debug.Log("Success!");
                    actualRarity = actualRarity + shopManager.ReturnBasicRarities(i);
                }
                else{
                    Debug.Log("ShopManagerNotFound");
                }
            }
            rarities[currentRarity] = actualRarity;
        }
        isBasic = true;
        Debug.Log("Is Basic");
        if(rand <= rarities[0]){
            PickCommonItem();
        }
        else if(rand <= rarities[1]){
            PickRareItem();
        }
        else if(rand <= rarities[2]){
            PickEpicItem();
        }
        else if(rand > rarities[2]){
            PickLegendaryItem();
        }
    }
    void GetDeluxeRarity(int rand){
        for(int currentRarity = 0; currentRarity < rarities.Length; currentRarity++){
            float actualRarity = 0;
            for(int i = 0; i < currentRarity + 1; i++){
                actualRarity = actualRarity + shopManager.ReturnDeluxeRarities(i);
            }
            rarities[currentRarity] = actualRarity;
        }
        isBasic = false;
        if(rand <= rarities[0]){
            PickCommonItem();
        }
        else if(rand <= rarities[1]){
            PickRareItem();
        }
        else if(rand <= rarities[2]){
            PickEpicItem();
        }
        else if(rand <= rarities[3]){
            PickLegendaryItem();
        }
    }
    void GetProRarity(int rand){
        for(int currentRarity = 0; currentRarity < rarities.Length; currentRarity++)
        {
            float actualRarity = 0;
            for(int i = 0; i < currentRarity + 1; i++)
            {
                actualRarity = actualRarity + shopManager.ReturnProRarities(i);
            }
            rarities[currentRarity] = actualRarity;
        }
        isBasic = false;
        if(rand <= rarities[0]){
            PickCommonItem();
        }
        else if(rand <= rarities[1]){
            PickRareItem();
        }
        else if(rand <= rarities[2]){
            PickEpicItem();
        }
        else if(rand <= rarities[3]){
            PickLegendaryItem();
        }
    }
    void PickCommonItem(){
        int rand = Random.Range(1, shopManager.commonAccessories.Length + shopManager.commonPatterns.Length);
        if(rand <= shopManager.commonAccessories.Length){
            GetAccessory("common");
        }
        else if(rand > shopManager.commonAccessories.Length){
            GetPattern("common");
        }
    }
    void PickRareItem(){
        int rand = Random.Range(1, shopManager.rareAccessories.Length + shopManager.rarePatterns.Length);
        if(rand <= shopManager.rareAccessories.Length){
            GetAccessory("rare");
        }
        else if(rand > shopManager.rareAccessories.Length){
        GetPattern("rare");
        }
    }
    void PickEpicItem(){
        int rand = Random.Range(1, shopManager.epicAccessories.Length + shopManager.epicPatterns.Length);
        if(rand <= shopManager.epicAccessories.Length){
            GetAccessory("epic");
        }
        else if(rand > shopManager.epicAccessories.Length){
            GetPattern("epic");
        }
    }
    void PickLegendaryItem(){
        int rand = Random.Range(1, shopManager.legendaryAccessories.Length + shopManager.legendaryPatterns.Length);
        if(rand <= shopManager.legendaryAccessories.Length){
            GetAccessory("legendary");
        }
        else if(rand > shopManager.legendaryAccessories.Length){
            GetPattern("legendary");
        }
    }
    void GetAccessory(string rarity){
        if(rarity == "common"){
            int rand = Random.Range(0, shopManager.commonAccessories.Length);
            cosmeticWon = shopManager.commonAccessories[rand];
        }
        if(rarity == "rare"){
            int rand = Random.Range(0, shopManager.rareAccessories.Length);
            cosmeticWon = shopManager.rareAccessories[rand];
        }
        if(rarity == "epic"){
            int rand = Random.Range(0, shopManager.epicAccessories.Length);
            cosmeticWon = shopManager.epicAccessories[rand];
        }
        if(rarity == "legendary"){
            int rand = Random.Range(0, shopManager.legendaryAccessories.Length);
            cosmeticWon = shopManager.legendaryAccessories[rand];
        }
        Debug.Log(cosmeticWon + rarity);
        isAccessory = true;
        if(!isBasic && catalogue.CheckIfOwnedAccessory(cosmeticWon)){
        GetAccessory(rarity);
        }
        else if(!catalogue.CheckIfOwnedAccessory(cosmeticWon)){
            shopManager.UpdateAccessoryWon(cosmeticWon);
            Debug.Log("Accessory " + cosmeticWon);
        }
    }
    void GetPattern(string rarity){
        if(rarity == "common"){
            int rand = Random.Range(0, shopManager.commonPatterns.Length);
            cosmeticWon = shopManager.commonPatterns[rand];
        }
        if(rarity == "rare"){
            int rand = Random.Range(0, shopManager.rarePatterns.Length);
            cosmeticWon = shopManager.rarePatterns[rand];
        }
        if(rarity == "epic"){
            int rand = Random.Range(0, shopManager.epicPatterns.Length);
            cosmeticWon = shopManager.epicPatterns[rand];
        }
        if(rarity == "legendary"){
            int rand = Random.Range(0, shopManager.legendaryPatterns.Length);
            cosmeticWon = shopManager.legendaryPatterns[rand];
        }
        Debug.Log(cosmeticWon);
        isAccessory = false;
        if(!isBasic && catalogue.CheckIfOwnedPattern(cosmeticWon)){
            GetPattern(rarity);
        }
        else if(!catalogue.CheckIfOwnedPattern(cosmeticWon)){
            shopManager.UpdatePatternWon(cosmeticWon);
            Debug.Log("Is Pattern " + cosmeticWon);
        }
    }

    public int ReturnCosmeticWon(){
        Debug.Log(cosmeticWon);
        return cosmeticWon;
    }
    public bool ReturnIsAccessory(){
        return isAccessory;
    }
    public void UpdatePackageSelection(int index){
        packageSelected = index;
        if(packageSelected == 0){
            Debug.Log("Selected: Basic");
        }
        if(packageSelected == 1){
            Debug.Log("Selected: Deluxe");
        }
        if(packageSelected == 2){
            Debug.Log("Selected: Pro");
        }
    }
}