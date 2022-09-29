using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class UIRepo
{
[SerializeField]Ears ears;
[SerializeField]EndScreen endScreen;
[SerializeField]OptionsMenu optionsMenu;
[SerializeField]PatternBar patternBar;
[SerializeField]StartMenu startMenu;
[SerializeField]TutorialScreen tutorialScreen;
[SerializeField]UITweener uITweener;
[SerializeField]LoadIcon loadIcon;
TestHelper testHelper;
public void SetTestHelper(TestHelper helper){
    testHelper = helper;
}
public EndScreen GetEndScreen(){
    if(endScreen == null){
        testHelper.LogError("Error: End Screen not found!");
        return null;
    }
    return endScreen;
}
public TutorialScreen GetTutorialScreen(){
    if(tutorialScreen == null){
        testHelper.LogError("Error: Tutorial Screen not found!");
        return null;
    }
    return tutorialScreen;
}
public StartMenu GetStartMenu(){
    if(startMenu == null){
        testHelper.LogError("Error: Start Menu not found!");
        return null;
    }
    return startMenu;
}

public OptionsMenu GetOptionsMenu(){
    if(optionsMenu == null){
        testHelper.LogError("Error: Options Menu not found!");
        return null;
    }
    return optionsMenu;
}
public LoadIcon GetLoadIcon(){
    if(loadIcon == null){
        testHelper.LogError("Error: Load Icon not found!");
        return null;
    }
    return loadIcon;
}
}
