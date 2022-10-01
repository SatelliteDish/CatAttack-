using UnityEngine;
using UnityEngine.UI;
using TMPro;
using State;
public class EndScreen : MonoBehaviour
{
[SerializeField]TextMeshProUGUI highscoreText;
[SerializeField]Button respawnButton;
GameManager gameManager;
StateController stateController;
void Start(){
    GetReferences();
    highscoreText.text = "Game Over!\nHighscore:\n"+ PlayerPrefs.GetInt("Highscore", 0).ToString();
}
void GetReferences(){
    ManagersRepo managersRepo = FindObjectOfType<DependencyManager>().GetManagersRepo();
    gameManager = managersRepo.GetGameManager();
    stateController = managersRepo.GetStateController();
}
void Update()
{
    if(stateController.GetState(StateType.hasRespawned) && gameObject.activeInHierarchy == true){
        respawnButton.gameObject.SetActive(false);
    }
}
public void UpdateHighScoreText(){
    highscoreText.text = "Game Over!\nHighscore:\n"+ PlayerPrefs.GetInt("Highscore", 0).ToString();
}
}
