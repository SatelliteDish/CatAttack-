using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadIcon : MonoBehaviour
{
[SerializeField]Animator anim;
[SerializeField] TextMeshProUGUI loadText;
[SerializeField]float delayTime = .2f;
[SerializeField]UITweener uITweener;
string loadStart = "Loading";
void Start(){
    loadText.text = loadStart;
    StartCoroutine(Dot());
}
IEnumerator Dot(){
    yield return new WaitForSecondsRealtime(delayTime);
    loadText.text = "Loading .";
    StartCoroutine(DotDot());
}
IEnumerator DotDot(){
    yield return new WaitForSecondsRealtime(delayTime);
    loadText.text = "Loading . .";
    StartCoroutine(DotDotDot());
}   
IEnumerator DotDotDot(){
    yield return new WaitForSecondsRealtime(delayTime);
    loadText.text = "Loading . . .";
    StartCoroutine(ResetText());
}
IEnumerator ResetText(){
    yield return new WaitForSecondsRealtime(delayTime);
}
public void UpdateLoadStatus(bool status){
    anim.SetBool("isLoading", !status);
}
public void StartLoad(){
    anim.SetBool("isLoading", true);
}
public UITweener GetUITweener(){
if(uITweener == null){
    Debug.LogError("Error: UI Tweener not found!");
    return null;
}
return uITweener;
}
}
