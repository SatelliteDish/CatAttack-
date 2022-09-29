using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ears : MonoBehaviour
{
MenuController menuController;
DependencyManager dependencyManager;
Animator myAnimator;
void Start(){
GetReference();
myAnimator.SetBool("Left Ear", false);
myAnimator.SetBool("Right Ear", false);    
StartCoroutine(Twitch());
}
void GetReference(){
    dependencyManager = FindObjectOfType<DependencyManager>();
    menuController = dependencyManager.GetManagersRepo().GetMenuController();
    myAnimator = GetComponent<Animator>();
}

IEnumerator Twitch()
{
    yield return new WaitForSecondsRealtime(Random.Range(1, menuController.ReturnMaxTwitchDelay()));
    int ear = Random.Range(0,2);
    if(ear == 0)
    {myAnimator.SetBool("Right Ear", true);}
    else if(ear == 1){myAnimator.SetBool("Left Ear", true);}
    StartCoroutine(ReturnToDefault(ear));
}
IEnumerator ReturnToDefault(int side)
{
    yield return new WaitForSecondsRealtime(Random.Range(1, menuController.ReturnMaxTwitchDelay()));
    myAnimator.SetBool("Right Ear", false);
    myAnimator.SetBool("Left Ear", false);
    StartCoroutine(Twitch());
}
}
