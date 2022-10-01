using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using State;

public class GameManager : MonoBehaviour, IObserver<PlayerStates<GameManager>>
{
    [Header("Game Settings")]
    public GameObject[] situations;
    [SerializeField]int boostCount = 2;
    [Range(0, 10)]
    [SerializeField]float slowSpeed = 2f;
    [Range(0, 10)]
    [SerializeField]float fastSpeed = 2f;
    [SerializeField]float respawnTime = 2f;
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
    DependencyManager<T> dependencyManager;
    EndScreen endScreen;
    [SerializeField]Player<GameManager> player;
    CosmeticsCatalogue cosmetics;
    CurrencyTracker currencyTracker;
    CameraManager cameraManager;
    BGManager bgManager;
    bool playerIsAlive = true;
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
            cameraManager.SetCamFollow(CameraType.Main);
            cameraManager.SwitchToMainCam();
        }
        DisableBoostAllIndicators();
    }
    void GetReferences(){
        dependencyManager = FindObjectOfType<DependencyManager<T>>();
        ManagersRepo<T> managersRepo = dependencyManager.GetManagersRepo();;
        CosmeticsRepo cosmeticsRepo = dependencyManager.GetCosmeticsRepo();;
        UIRepo uIRepo = dependencyManager.GetUIRepo();
        WorldGenerationRepo worldGenerationRepo = dependencyManager.GetWorldGenerationRepo();
        if(uIRepo.GetEndScreen() != null){
            endScreen = uIRepo.GetEndScreen();
        }
        audioManager = managersRepo.GetAudioManager();
        cosmetics = cosmeticsRepo.GetCosmeticsCatalogue();
        currencyTracker = managersRepo.GetCurrencyTracker();
        cameraManager = managersRepo.GetCameraManager();
        bgManager = managersRepo.GetBGManager();
        loadIcon = uIRepo.GetLoadIcon();
        stateController = managersRepo.GetStateController();
    }
    public void QuitGame(){
        StartCoroutine(ReloadLevel(0));
    }
    public GameObject GetRandomSituation(){
        return situations[Random.Range(0,situations.Length)];
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
        SaveSystem.SavePlayer(cosmetics);
    }
    public void LoadData(){
        SaveData data = SaveSystem.LoadCosmeticData();
        cosmetics.cosmeticsOwned = data.cosmeticsOwned;
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

    public float ReturnSlowSpeed(){
        return slowSpeed;
    }

    public float ReturnFastSpeed(){
        return fastSpeed;
    }

    public float ReturnRespawnTime(){
        return respawnTime;
    }

     public float ReturnMinGravity(){
        return minGravity;
    }
     public float ReturnGravityMultiplier(){
        return gravityMultiplier;
    }
     public int ReturnBoostCount(){
        return boostCount;
    }
     public void UpdateCurrencyText(int count){
        if(currencyCountText != null){currencyCountText.text = count.ToString();}
    }

    void PlayerDeath(bool val) {
        if(val == true) {
            cameraManager.SwitchToDeadCam();
            audioManager.Pause("Music");
            audioManager.Play("Die");
            ShowEndScreen(true); 
        }
        else {
            cameraManager.SwitchToMainCam();
            audioManager.Play("Music");
            ShowEndScreen(false);
        }
    }
    void IObserver<PlayerStates<GameManager>>.ObserverUpdate() {
        PlayerDeath(!player.states.IsAlive());
    }
}