using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum CanvasType{
    SkipTutorial,
    StartMenu,
    OptionsMenu
};
public class MenuController : MonoBehaviour{
[SerializeField]string title;
StartMenu startMenu;
OptionsMenu optionsMenu;
[SerializeField]Canvas skipTutorial;
[SerializeField]GameObject[] spawnLocations;
[SerializeField]GameObject[] catPositon;
[SerializeField]int maxTwitchDelay;
GameManager gameManager;
[SerializeField]UITweener loadScreen;
LoadIcon loadIcon;
DependencyManager<T> dependencyManager;
void Start(){
    GetReferences();
    ChangeCanvas(CanvasType.StartMenu);
    if(spawnLocations.Length == catPositon.Length){
        int rand = Random.Range(0, spawnLocations.Length);
        var cat = Instantiate(catPositon[rand], spawnLocations[rand].transform.position, spawnLocations[rand].transform.rotation);
        cat.transform.parent = gameObject.transform;
    }
}
public string ReturnTitle(){
    return title;
}
void GetReferences(){
    dependencyManager = FindObjectOfType<DependencyManager<T>>();
    startMenu = dependencyManager.GetUIRepo().GetStartMenu();
    optionsMenu = dependencyManager.GetUIRepo().GetOptionsMenu();
    gameManager = dependencyManager.GetManagersRepo().GetGameManager();
    loadIcon = dependencyManager.GetUIRepo().GetLoadIcon();
}
public void SetCanvas(string name){
    if(name == "startMenu"){
        ChangeCanvas(CanvasType.StartMenu);
    }
    else if(name == "optionsMenu"){
        ChangeCanvas(CanvasType.OptionsMenu);
    }
    else if(name == "skipTutorial"){
        ChangeCanvas(CanvasType.SkipTutorial);
    }
    else{
        Debug.LogWarning("Warning: " + name + " not found!");
    }
}
void ChangeCanvas(CanvasType type){
    SetTutorialCanvas(type);
    SetOptionsCanvas(type);
    SetStartCanvas(type);
}
void SetTutorialCanvas(CanvasType type){
    if(type == CanvasType.SkipTutorial){
        skipTutorial.gameObject.SetActive(true);
        return;
    }
    skipTutorial.gameObject.SetActive(false);
}
void SetStartCanvas(CanvasType type){
    if(type == CanvasType.StartMenu){
        startMenu.gameObject.SetActive(true);
        return;
    }
    startMenu.gameObject.SetActive(false);
}
void SetOptionsCanvas(CanvasType type){
    if(type == CanvasType.OptionsMenu){
        optionsMenu.gameObject.SetActive(true);
        return;
    }
    optionsMenu.gameObject.SetActive(false);
}
public void StartGame(){
    if(PlayerPrefs.GetString("hasCompletedTutorial") != "true"){
        ChangeCanvas(CanvasType.SkipTutorial);
        PlayerPrefs.SetInt("Current Accessory", 0);
        PlayerPrefs.SetInt("Current Pattern", 0);
    }
    else{
        gameManager.ReplayLevel(1);
        loadScreen.OnButtonPress("in");
    }
}
public void SkipTutorial(){
    PlayerPrefs.SetString("hasCompletedTutorial", "true");
    gameManager.ReplayLevel(1);
    loadScreen.OnButtonPress("in");
}
public void TutorialStart(){
    gameManager.ReplayLevel(2);
}
IEnumerator StartTutorial(){
    AsyncOperation operation = SceneManager.LoadSceneAsync(2);
    while(!operation.isDone){
        float progress = Mathf.Clamp01(operation.progress/ 0.9f);
        if(progress < 1){loadIcon.UpdateLoadStatus(true);}
        yield return null;
    }
    while(!operation.isDone)
    {
        float progress = Mathf.Clamp01(operation.progress/ 0.9f);
        if(progress < 1){loadIcon.UpdateLoadStatus(true);}
        yield return null;
    }    
}
public void ResetHighscore(){
    PlayerPrefs.SetInt("Highscore", 0);
    PlayerPrefs.SetString("hasCompletedTutorial", "false");
}
public void Customize(){
    loadIcon.gameObject.SetActive(true);
    loadScreen.GetComponent<UITweener>().OnButtonPress("out");
    StartCoroutine(LoadShop());
}
IEnumerator LoadShop(){
    AsyncOperation operation = SceneManager.LoadSceneAsync(3);
    while(!operation.isDone){
        float progress = Mathf.Clamp01(operation.progress/ 0.9f);
        if(progress < 1){
            loadIcon.UpdateLoadStatus(true);
        }
        yield return null;
    }  
}
public int ReturnMaxTwitchDelay(){
    return maxTwitchDelay;
}
}
