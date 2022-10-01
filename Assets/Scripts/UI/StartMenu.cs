using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartMenu : MonoBehaviour
{
[SerializeField]TextMeshProUGUI titleText;
MenuController menuController;
DependencyManager<StartMenu> dependencyManager;
void Start(){
    GetReference();
    menuController = FindObjectOfType<MenuController>();
    titleText.text = menuController.ReturnTitle();
}
void GetReference(){
    dependencyManager = FindObjectOfType<DependencyManager<StartMenu>>();
    menuController = dependencyManager.GetManagersRepo().GetMenuController();
}
}
