using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Tutorial;
public class TutorialScreen : MonoBehaviour
{
    [SerializeField]UITweener loadScreen;
    [SerializeField]string jumpText;
    [SerializeField]string boostText;
    [SerializeField]string targetText;
    
    [SerializeField]GameObject perfLine;
    [SerializeField]TextMeshProUGUI leftText;
    [SerializeField]TextMeshProUGUI rightText;
    [SerializeField]TextMeshProUGUI centreText;
    [SerializeField]float textDelay = .5f;
    [SerializeField]GameObject[] targets;
    [SerializeField]ObjectSpawner spawner;
    [SerializeField]GameObject coin;
    [SerializeField]GameObject[] hazards;
    [SerializeField]Button backToMenu;
    [SerializeField]Player<TutorialScreen> player;
    bool isInPosition = false;
    private void Start(){
        SetBaseState();
    }
    void SetBaseState(){
        leftText.gameObject.SetActive(false);
        rightText.gameObject.SetActive(false);
        centreText.gameObject.SetActive(false);
        perfLine.gameObject.SetActive(true);
        backToMenu.gameObject.SetActive(false);
    }
    private void Update() {
        while(!isInPosition){
            player.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(Time.deltaTime /* .5f*/,0);
            if(player.transform.position.x == 0){
                isInPosition = true;
        }
        }
    }
    public void SetCompletion(TutorialStates state){
        switch(state){
            case TutorialStates.JumpComplete:
                break;
            case TutorialStates.BoostComplete:
                break;
            case TutorialStates.FallComplete:
                break;
            case TutorialStates.HasMoved:
                break;
            case TutorialStates.CoinCollected:
                break;
            case TutorialStates.CoinMissed:
                break;
        }
        
    }
    private void TogglePerfLine(){
        perfLine.gameObject.SetActive(!perfLine.gameObject.activeSelf);
    }
}
