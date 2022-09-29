using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CosmeticsRepo
{
[SerializeField]CosmeticPicker cosmeticPicker;
[SerializeField]CosmeticsCatalogue cosmeticsCatalogue;
[SerializeField]CosmeticsScreen cosmeticsScreen;
[SerializeField]CosmeticsSettings cosmeticsSettings;
[SerializeField]ShopManager shopManager;
TestHelper testHelper;
public void SetTestHelper(TestHelper helper){
    testHelper = helper;
}
public CosmeticsCatalogue GetCosmeticsCatalogue(){
    if(cosmeticsCatalogue == null){
        testHelper.LogWarning("Error: Cosmetics Catalogue not found!");
    }
    return cosmeticsCatalogue;
}
public CosmeticsSettings GetCosmeticsSettings(){
    if(cosmeticsSettings == null){
        testHelper.LogWarning("Error: Cosmetic Settings Not Found");
        throw new System.Exception();
    }
    return cosmeticsSettings;
}
public void SetCosmeticsSettings(CosmeticsSettings settings){
    if(settings == null){
        testHelper.LogError("Error: Reference to Cosmetics Settings not found!");
        return;
    }
    cosmeticsSettings = settings;
}
public ShopManager GetShopManager(){
    if(shopManager == null){
        testHelper.LogError("Error: Shop Manager not found!");
        return null;
    }
    return shopManager;
}
public CosmeticsScreen GetCosmeticsScreen(){
    if(cosmeticsScreen == null){
        testHelper.LogError("Error: Cosmetics Screen not found!");
        return null;
    }
    return cosmeticsScreen;
}
}