using UnityEngine;
using State;
[System.Serializable]
public class ManagersRepo
{
[SerializeField]GameManager gameManager;
[SerializeField]BGManager bGManager;
[SerializeField]AnimationController animationController;
[SerializeField]ControlManager controlManager;
[SerializeField]AudioManager audioManager;
[SerializeField]CameraManager cameraManager;
[SerializeField]DependencyManager dependencyManager;
[SerializeField]MenuController menuController;
[SerializeField]CurrencyTracker currencyTracker;
[SerializeField]SpeedController speedController;
[SerializeField]StateUpdater stateUpdater;
[SerializeField]StateController stateController;
[SerializeField]TestHelper testHelper;
[SerializeField]SpawnerHelper spawnerHelper;
public SpawnerHelper GetSpawnerHelper(){
    return spawnerHelper;
}
public void SetSpawnerHelper(SpawnerHelper helper){
    if(helper != null){
    spawnerHelper = helper;
    }
}
public TestHelper GetTestHelper(){
    if(testHelper == null){
        if(!stateController.GetState(StateType.isTest)){
            Debug.LogWarning("Warning: Test Helper not found!");
        }
    }
    return testHelper;
}
public void SetTestHelper(TestHelper helper){
    if(helper == null){
        testHelper.LogWarning("Warning: Attempted to set Test Helper to null!");
        return;
    }
    testHelper = helper;
}
public StateController GetStateController(){
    if(stateController == null){
        testHelper.LogWarning("Warning: State Controller not found!");
    }
    return stateController;
}
public void SetStateController(StateController controller){
    if(controller == null){
        testHelper.LogWarning("Warning: State Controller not set, invalid value!");
    }
    stateController = controller;
}
public StateUpdater GetStateUpdater(){
    if(stateUpdater == null){
        testHelper.LogWarning("Warning: State Updater not found!");
    }
    return stateUpdater;
}
public void SetStateUpdater(StateUpdater updater){
    if(updater == null){
        testHelper.LogWarning("Warning: Invalid State Updater, State Updater not set.");
        return;
    }
    stateUpdater = updater;
}
public SpeedController GetSpeedController(){
    if(speedController == null){
        testHelper.LogWarning("Warning: Speed Controller not found!");
    }
    return speedController;
}
public CurrencyTracker GetCurrencyTracker(){
    if(currencyTracker == null){
        testHelper.LogWarning("Warning: Currency Tracker not found!");
    }
    return currencyTracker;
}
public CameraManager GetCameraManager(){
    if(cameraManager == null){
        testHelper.LogWarning("Warning: Camera Manager not found!");
    }
    return cameraManager;
}
public BGManager GetBGManager(){
    if(bGManager == null){
        testHelper.LogWarning("Warning: BG Manager not found!");
    }
    return bGManager;
}
public GameManager GetGameManager(){
    if(gameManager == null){
        testHelper.LogWarning("Warning: Game Manager not found!");
    }
    return gameManager;
}
public AnimationController GetAnimationController(){
    if(animationController == null){
        testHelper.LogWarning("Warning: Animation Controller not found.");
    }
    return animationController;
}
public AudioManager GetAudioManager(){
    if(audioManager == null){
        testHelper.LogWarning("Warning: Audio Manager not found!");
    }
    return audioManager;
}
public void SetAudioManager(AudioManager manager){
    if(manager == null){
        testHelper.LogWarning("Warning: Reference to Audio Manager not found!");
        return;
    }
    audioManager = manager;
}
public void SetControlManager(ControlManager control){
    if(control == null){
        testHelper.LogWarning("Warning: Reference to Control Manager not found!");
        return;
    }
    controlManager = control;
}
public ControlManager GetControlManager(){
    if(controlManager == null){
        testHelper.LogWarning("Warning: Control Manager not found!");
    }
    return controlManager;
}
public MenuController GetMenuController(){
    if(menuController == null){
        testHelper.LogWarning("Warning: Menu Controller not found!");
    }
    return menuController;
}
}
