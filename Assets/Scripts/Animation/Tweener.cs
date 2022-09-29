using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    [SerializeField]ITweening tween;             
    
    public void Play(){
        tween.Play(gameObject);
    }
}
