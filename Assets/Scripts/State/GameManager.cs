using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using State;

public class GameManager : MonoBehaviour {
    [Header("Game Settings")]
    [SerializeField]float gravityMultiplier = .1f;
    [SerializeField]float minGravity = 4f;
    [Header("Interactables")]
    [SerializeField]float[] odds;
    [SerializeField]int[] pointValue;
    [Header("Spawn Locations")]
    [SerializeField]GameObject groundParent;
    [Header("UI")]
    [SerializeField]TextMeshProUGUI scoreText;
    [SerializeField]TextMeshProUGUI currencyCountText;
    [SerializeField]Image[] boostIndicator;
    [SerializeField]GameObject loadScreen;
    StateController stateController;
    LoadIcon loadIcon;
    AudioManager audioManager;
    DependencyManager dependencyManager;
    EndScreen endScreen;
    [SerializeField]Player player;
    CurrencyTracker currencyTracker;
    CameraManager cameraManager;
    BGManager bgManager;
    int score = 0;
    void Start(){ 
        FindObjectOfType<AudioManager>().Play("Music");
        GetReferences();
        if(currencyCountText != null){
            currencyCountText.text = currencyTracker.ReturnCurrencyCount().ToString();
        }
        if(endScreen != null){
            endScreen.gameObject.SetActive(false);
        }
        if(cameraManager != null){
            try {
                cameraManager.SetCamFollow("current",player.gameObject.transform);
                cameraManager.SwitchCam("main");
            }
            catch(ArgumentException error){
                Debug.LogError(error.Message);
            }
        }
        DisableBoostAllIndicators();
    }
    void GetReferences(){
        dependencyManager = FindObjectOfType<DependencyManager>();
        ManagersRepo managersRepo = dependencyManager.GetManagersRepo();;
        CosmeticsRepo cosmeticsRepo = dependencyManager.GetCosmeticsRepo();;
        UIRepo uIRepo = dependencyManager.GetUIRepo();
        WorldGenerationRepo worldGenerationRepo = dependencyManager.GetWorldGenerationRepo();
        if(uIRepo.GetEndScreen() != null){
            endScreen = uIRepo.GetEndScreen();
        }
        audioManager = managersRepo.GetAudioManager();
        currencyTracker = managersRepo.GetCurrencyTracker();
        cameraManager = managersRepo.GetCameraManager();
        bgManager = managersRepo.GetBGManager();
        loadIcon = uIRepo.GetLoadIcon();
        stateController = managersRepo.GetStateController();
    }
    public void QuitGame(){
        StartCoroutine(ReloadLevel(0));
    }
    void DisableBoostAllIndicators(){
        if(boostIndicator[0] == null){
            return;
        }
        for(int i =0; i < boostIndicator.Length; i++){
            boostIndicator[i].gameObject.SetActive(false);
        }
    }
    void Update(){
        if(boostIndicator[0] == null | player == null){
            return;
        }
        for(int i = 0; i < player.config.BoostLimit(); i++){
            boostIndicator[i].gameObject.SetActive(true);
        }
    }
    public void DisableBoostIndicator(int index){
        if(index < 0 || index > boostIndicator.Length) {
            Debug.Log("Incorrect indicator disabled! Attempted to disable " + index);
            return;
        }
        boostIndicator[index].gameObject.SetActive(false);
    }
    public void ShowEndScreen(bool active){
        if(active == true && endScreen != null){
            endScreen.gameObject.SetActive(true);
            stateController.SetState(StateType.isAlive, false);
            bgManager.UpdateBGSpeed(0);
        }
        if(active == false && endScreen != null){
            endScreen.gameObject.SetActive(false);
            stateController.SetState(StateType.isAlive, true);
        }
    }

    public void ReplayLevel(int sceneIndex){
        if(loadScreen != null){
        loadScreen.SetActive(true);
        loadScreen.GetComponent<UITweener>().OnButtonPress("out");}
        else{
            Debug.Log("loadScreen not found");
        }
        StartCoroutine(ReloadLevel(sceneIndex));
    }
    IEnumerator ReloadLevel(int sceneIndex){
        if(PlayerPrefs.GetString("hasCompletedTutorial") == "true"){
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
            while(!operation.isDone){
                float progress = Mathf.Clamp01(operation.progress/ 0.9f);
                if(progress < 1 && loadIcon != null){
                    loadIcon.UpdateLoadStatus(true);
                }
                yield return null;
            }  
        }
        else{
            AsyncOperation operation = SceneManager.LoadSceneAsync(2);
            while(!operation.isDone){
                float progress = Mathf.Clamp01(operation.progress/ 0.9f);
                if(progress < 1 && loadIcon != null){
                    loadIcon.UpdateLoadStatus(true);
                }
                yield return null;
            }    
        }
    }
    public void AddScore(int points){
        if(scoreText == null){
            return;
        }
        score = score + points;
        scoreText.text = "Score:" + score;
        if(score > PlayerPrefs.GetInt("Highscore", 0) && endScreen != null){
            UpdateHighscore();endScreen.UpdateHighScoreText();
        }
    }
    public void SaveData(){

    }
    public void LoadData(){

    }
    public float ReturnOdds(int index){
        return odds[index];
    }
    public int ReturnPoints(int value){
        return pointValue[value];
    }

    void UpdateHighscore(){
        PlayerPrefs.SetInt("Highscore", score);
    }
    public GameObject ReturnGroundParent(){
        return groundParent;
    }
     public float ReturnMinGravity(){
        return minGravity;
    }
     public float ReturnGravityMultiplier(){
        return gravityMultiplier;
    }
     public void UpdateCurrencyText(int count){
        if(currencyCountText != null){currencyCountText.text = count.ToString();}
    }

    public void PlayerDeath(bool val) {
        if(val == true) {
            try {
                cameraManager.SwitchCam("dead");
                audioManager.Pause("Music");
                audioManager.Play("Die");
                ShowEndScreen(true); 
            }
            catch(ArgumentException error) {
                Debug.LogError(error.Message);
            }
        }
        else {
            try {
                cameraManager.SwitchCam("main");
                audioManager.Play("Music");
                ShowEndScreen(false);
            }
            catch(ArgumentException error) {
                Debug.LogError(error.Message);
            }
        }
    }
}