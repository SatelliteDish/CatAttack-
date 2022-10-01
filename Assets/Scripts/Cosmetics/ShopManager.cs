using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour {
    [SerializeField]int[] price;
    [SerializeField]TextMeshProUGUI[] priceText;
    [SerializeField]GameObject shopCanvas;
    [SerializeField]GameObject unlockCanvas;
    [SerializeField]GameObject mainCanvas;
    [SerializeField]GameObject prizeCanvas;
    [SerializeField]Image prizeIcon;
    [SerializeField]GameObject[] unlockBoxes;
    [SerializeField]Color32[] boxColors;
    [SerializeField]Color32[] rarityColors;
    public float[] basicRarities;
    [SerializeField]TextMeshProUGUI[] basicRaritiesText;
    [SerializeField]float[] deluxeRarities;
    [SerializeField]TextMeshProUGUI[] deluxeRaritiesText;
    [SerializeField]float[] proRarities;
    [SerializeField]TextMeshProUGUI[] proRaritiesText;
    CurrencyTracker currencyTracker;
    [SerializeField]CosmeticsCatalogue cosmeticsCatalogue;
    [SerializeField]Button boxSprite;
    [SerializeField]string[] rarityNames;
    public int[] commonAccessories;
    public int[] rareAccessories;
    public int[] epicAccessories;
    public int[] legendaryAccessories;
    public int[] commonPatterns;
    public int[] rarePatterns;
    public int[] epicPatterns;
    public int[] legendaryPatterns;
    [SerializeField] int cosmeticWon;
    //[SerializeField]bool isAccessory;
    [SerializeField]string cosmeticType;
    DependencyManager dependencyManager;
    [SerializeField]CosmeticPicker cosmeticPicker;

    void Awake(){
        BackToMainScreen();
    }
    void Start(){
        GetReferences();
        //sets price
        for(int i = 0; i < priceText.Length; i++){
            priceText[i].text = price[i].ToString();
        }
        //sets rarity
        for(int i = 0; i < basicRarities.Length; i++){
            basicRaritiesText[i].text = basicRarities[i].ToString() + "% " + rarityNames[i];
            deluxeRaritiesText[i].text = deluxeRarities[i].ToString() + "% " + rarityNames[i];
            proRaritiesText[i].text = proRarities[i].ToString() + "% " + rarityNames[i];;
        }
        for(int i = 0; i < basicRarities.Length; i ++){
            basicRaritiesText[i].color = rarityColors[i];
            deluxeRaritiesText[i].color = rarityColors[i];
            proRaritiesText[i].color = rarityColors[i];
        }
    }
    void GetReferences(){
        dependencyManager = FindObjectOfType<DependencyManager>();
        currencyTracker = dependencyManager.GetManagersRepo().GetCurrencyTracker();
    }
    public void OnPurchase(int index){
        if(currencyTracker.ReturnCurrencyCount() - price[index] < 0){
            Debug.LogError("OUT OF MONEY");
            return;
        }
        cosmeticPicker.UpdatePackageSelection(index);
        currencyTracker.UpdateCurrencyCount(price[index]);
        shopCanvas.gameObject.SetActive(false);
        unlockCanvas.gameObject.SetActive(true);
        for(int i = 0; i < unlockBoxes.Length; i++){
            var box = Instantiate(boxSprite, new Vector3(unlockBoxes[i].transform.position.x, unlockBoxes[i].transform.position.y,0), Quaternion.identity);
            box.gameObject.transform.SetParent(unlockBoxes[i].transform);
            box.gameObject.GetComponent<Image>().color = boxColors[Random.Range(0, boxColors.Length)];
        }
    }
    public void UpdateAccessoryWon(int index){
        cosmeticWon =  index;
        //isAccessory = true;
        Debug.Log("Accessory " + cosmeticWon);
    }
    public void UpdatePatternWon(int index){
        cosmeticWon =  index;
        //isAccessory = false;
        Debug.Log("Pattern " + cosmeticWon);
    }
    void UpdateCosmeticType(bool accessory){
        if(accessory == true){
            cosmeticType = "accessory";
        }
        if(accessory == false){
            cosmeticType = "pattern";
        }
        Debug.Log(cosmeticType);
    }
    public void OnUnlock(){
        Debug.Log("Unlocking");
        if(cosmeticPicker.ReturnIsAccessory())
        {
            if(cosmeticsCatalogue != null){
                prizeIcon.sprite = cosmeticsCatalogue.ReturnAccessoryIcon(cosmeticWon);}
            else{
                Debug.Log("Catalogue Not Found");
            }
        }
        if(!cosmeticPicker.ReturnIsAccessory()){
            if(cosmeticsCatalogue != null){
                prizeIcon.sprite = cosmeticsCatalogue.ReturnPatternIcon(cosmeticWon);
            }
            else{
                Debug.Log("Catalogue Not Found");
            }
        }
    }
    public float ReturnBasicRarities(int index){
        return basicRarities[index];
    }
    public float ReturnDeluxeRarities(int index){
        return deluxeRarities[index];
    }
    public float ReturnProRarities(int index){
        return proRarities[index];
    }
    public void ChangeToPrizeScreen(){
        Debug.Log("Changing to Prize");
        unlockCanvas.gameObject.SetActive(false);
        prizeCanvas.gameObject.SetActive(true);
        if(unlockCanvas.activeInHierarchy){
            Debug.Log("Switch Failed: Canvas won't disable");
        }
        if(!prizeCanvas.activeInHierarchy){
            Debug.Log("Switch Failed: Canvas won't enable");
        }
    }
    public void ChangeToShopScreen(){
        Debug.Log("Changing to Shop");
        mainCanvas.SetActive(false);
        shopCanvas.SetActive(true);
        if(!shopCanvas.activeInHierarchy){
            Debug.Log("Switch Failed: Canvas won't enable");
        }
        else if(mainCanvas.activeInHierarchy){
            Debug.Log("Switch Failed: Canvas won't disable");
        }
        else{
            Debug.Log("Success!");
        }
    }
    public void BackToMainScreen(){
        Debug.Log("Changing to main");
        mainCanvas.SetActive(true);
        shopCanvas.SetActive(false);
        unlockCanvas.SetActive(false);
        prizeCanvas.SetActive(false);
        if(!mainCanvas.activeInHierarchy){
            Debug.Log("Switch Failed: Main canvas won't enable");
        }
        else if(shopCanvas.activeInHierarchy){
            Debug.Log("Switch Failed: Shop canvas won't disable");
        }
        else if(unlockCanvas.activeInHierarchy){
            Debug.Log("Switch Failed: Unlock canvas won't disable");
        }
        else if(prizeCanvas.activeInHierarchy){
            Debug.Log("Switch Failed: Prize canvas won't disable");
        }
        else{
            Debug.Log("Success!");
        }
    }
}