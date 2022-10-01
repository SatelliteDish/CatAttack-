using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternBar : MonoBehaviour {
    [SerializeField]Image[] iconImages;
    CosmeticsCatalogue catalogue;
    DependencyManager<PatternBar> dependencyManager;
    void Start(){
        GetReference();
        UpdatePatternIcons();
    }
    void GetReference(){
        dependencyManager = FindObjectOfType<DependencyManager<PatternBar>>();
        catalogue = dependencyManager.GetCosmeticsRepo().GetCosmeticsCatalogue();
    }
    public void UpdatePatternIcons(){
        if(PlayerPrefs.GetInt("Current Pattern")-1 <0){
            iconImages[0].sprite = catalogue.ReturnPatternIcon(iconImages.Length-2);
        }
        else{
            iconImages[0].sprite = catalogue.ReturnPatternIcon(PlayerPrefs.GetInt("Current Pattern")-1);
        }
        iconImages[1].sprite = catalogue.ReturnPatternIcon(PlayerPrefs.GetInt("Current Pattern"));
        if(PlayerPrefs.GetInt("Current Pattern")+1 >= iconImages.Length-1){
            iconImages[2].sprite = catalogue.ReturnPatternIcon(0);
        }
        else{
            iconImages[2].sprite = catalogue.ReturnPatternIcon(PlayerPrefs.GetInt("Current Pattern")+1);
        }
    }
}