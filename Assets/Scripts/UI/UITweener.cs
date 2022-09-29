using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITweener : MonoBehaviour
{
    [SerializeField]bool playOnStart = false;
    [SerializeField]bool destroyWhenComplete = false;
    [SerializeField]bool disableWhenComplete = false;
    [SerializeField]string direction;
    [SerializeField]float duration = 1f;
    [Header("Object Type")]
    [SerializeField]bool isCanvas = false;
    [SerializeField]bool isGameObject = false;
    [Header("Fade")]
    [SerializeField]bool fade = false;
    [Header("Move")]
    [SerializeField]bool move = false;
    [SerializeField]float floatDistance = 1f;
    [SerializeField]float floatTime = 1f;
    [SerializeField]bool loopFloat = false;
    string floatState;
void Start(){
    if(playOnStart){
        OnButtonPress(direction);
    }
}
public void OnButtonPress(string status){
    gameObject.SetActive(true);
    if(fade){Fade(status);}
    if(move){Move(status);}
}
void Move(string status){
    if(gameObject == null){
        return;
    }
    floatState = status;
    if(status == "up"){
        LeanTween.moveY(gameObject, gameObject.transform.position.y + floatDistance, floatTime).setOnComplete(OnComplete);
    }
    if(status == "down"){
        LeanTween.moveY(gameObject, gameObject.transform.position.y - floatDistance, floatTime).setOnComplete(OnComplete);
    }
}
void Fade(string status){
    if(gameObject == null){
        return;
    }
    if(status == "in"){
        if(isCanvas){
            LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 0f, duration).setDelay(duration).setOnComplete(OnComplete);
        }
        if(isGameObject){
            LeanTween.alpha(gameObject, 0f, duration).setDelay(duration).setOnComplete(OnComplete);
        }
    }
    if(status == "out"){
        if(isCanvas){
            LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 1f, duration).setDelay(duration);
        }
        if(isGameObject){
            LeanTween.alpha(gameObject, 1f, duration).setDelay(duration);
        }
    }
    Invoke("FloatFix", duration);
}
void FloatFix(){
    if(loopFloat == true){
        if(floatState == "up"){
            Move("down");
        }
        if (floatState == "down"){
            Move("up");
        }
    }
}
void OnComplete(){
    if(destroyWhenComplete == true){
        Destroy(gameObject);
    }
    if(disableWhenComplete == true){
        gameObject.SetActive(false);
    }
}
}