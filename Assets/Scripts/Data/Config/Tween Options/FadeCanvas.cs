using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class FadeCanvas : ITweening
{
    [SerializeField]float duration = 1f;
    [SerializeField]float finalOpacity = 0f;
    public void Play(GameObject obj){
        LeanTween.alphaCanvas(obj.GetComponent<CanvasGroup>(),finalOpacity,duration);
    }
    public void Reverse(GameObject obj){
        LeanTween.alphaCanvas(obj.GetComponent<CanvasGroup>(),finalOpacity,duration);
    }
}
