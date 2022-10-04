using UnityEngine;

public class AnimationController : MonoBehaviour {
    [SerializeField]Animator[] animator;
    void Start(){
        SetRun();
    }
    public void SetRun(){
        if(animator.Length  == 0){
            return;
        }
        for(int i = 0; i < animator.Length; i++) {
            animator[i].Play("Run", -1, 0);
        }
    }
    public void SetInAir(bool inAir){
        if(animator == null){
            return;
        }
        for(int i = 0; i < animator.Length; i++) {
            animator[i].SetBool("inAir", inAir);
        }
    }
}