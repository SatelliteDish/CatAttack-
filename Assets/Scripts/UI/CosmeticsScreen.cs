using UnityEngine;
using State;

public class CosmeticsScreen : MonoBehaviour
{
    CosmeticsCatalogue cosmetics;
    Player player;
    ControlManager controlManager;
    StateController stateController;
    DependencyManager dependencyManager;
    int currentAccessoryShown = 0;
    int currentPatternShown = 0;
    void Awake(){
    GetReferences();
    currentAccessoryShown = PlayerPrefs.GetInt("Current Accessory");
    SpawnPlayer();
   }
   void GetReferences(){
       dependencyManager = FindObjectOfType<DependencyManager>();
       cosmetics = dependencyManager.GetCosmeticsRepo().GetCosmeticsCatalogue();
   }
public void NextCharacter(bool isPattern){
    bool spawned = false;
    if(!isPattern){ //if this isn't changing pattern
        if(cosmetics.cosmeticsOwned.Length <= 1){ //and you have a accessory unlocked
            return;
        }
        if(currentAccessoryShown < cosmetics.cosmeticsOwned.Length - 1 && !spawned){ //if this isn't the last item in the list
            spawned = true;
            Destroy(player.gameObject);
            currentAccessoryShown++;     
            PlayerPrefs.SetInt("Current Accessory", currentAccessoryShown);
            SpawnPlayer();
        }
        else{
            Destroy(player.gameObject);
            currentAccessoryShown = 0;        
            PlayerPrefs.SetInt("Current Accessory", currentAccessoryShown);
            SpawnPlayer();
        }
    }
    if(isPattern){ //if you are changing pattern
        if(cosmetics.patternsOwned.Length == 1){return;} //and you have more than 1 option
        if(currentPatternShown < cosmetics.patternsOwned.Length - 1 && !spawned){//if this isn't the last item in the list
            spawned = true;
            Destroy(player.gameObject);
            currentPatternShown++;     
            PlayerPrefs.SetInt("Current Pattern", currentPatternShown);
            SpawnPlayer();
        }
        else{
            Destroy(player.gameObject);
            currentPatternShown = 0;        
            PlayerPrefs.SetInt("Current Pattern", currentPatternShown);
            SpawnPlayer();
        }
    }
}
public void PreviousCharacter(bool isPattern){
    bool spawned = false;
    if(!isPattern){ //if you aren't changing pattern
        if(cosmetics.cosmeticsOwned.Length == 1){ //and you own more than 1 accessory
            return;
        }
        if(currentAccessoryShown > 0 && !spawned){ //if this isn't the first item in the list
            spawned = true;
            Destroy(player.gameObject);
            currentAccessoryShown--;
            PlayerPrefs.SetInt("Current Accessory", currentAccessoryShown);
            SpawnPlayer();
        }
        else{
            Destroy(player.gameObject);
            currentAccessoryShown = cosmetics.cosmeticsOwned.Length - 1;   
            PlayerPrefs.SetInt("Current Accessory", currentAccessoryShown);
            SpawnPlayer();
        }
    }
    if(isPattern){ //if you are changing pattern
        if(cosmetics.patternsOwned.Length == 1){ //and you own more than 1 pattern
            return;
        }
        if(currentPatternShown > 0 && !spawned){
            spawned = true;
            Destroy(player.gameObject);
            currentPatternShown--;     
            PlayerPrefs.SetInt("Current Pattern", currentPatternShown);
            SpawnPlayer();
        }
        else{
            Destroy(player.gameObject);
            currentPatternShown = cosmetics.patternsOwned.Length -1;        
            PlayerPrefs.SetInt("Current Pattern", currentPatternShown);
            SpawnPlayer();
        }
    }
}
void SpawnPlayer(){
    Instantiate(cosmetics.ReturnOwnedPattern(PlayerPrefs.GetInt("Current Pattern")), new Vector3(0, 0, 0), Quaternion.identity);
    player = dependencyManager.GetWorldGenerationRepo().GetPlayer();
    stateController.SetState(StateType.canMove, false);
}
}
    
