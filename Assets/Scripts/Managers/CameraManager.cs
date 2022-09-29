using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public enum CameraType {Main, Dead};
public class CameraManager : MonoBehaviour
{
[SerializeField]CinemachineVirtualCamera mainCam;
[SerializeField]CinemachineVirtualCamera deadCam;
[SerializeField]Camera myCamera;

void Start(){
    SwitchToMainCam();
}
public void SwitchToMainCam(){
    if(deadCam.isActiveAndEnabled){
        mainCam.gameObject.transform.position = deadCam.gameObject.transform.position;
        deadCam.gameObject.SetActive(false);
    }
    mainCam.gameObject.SetActive(true);
}
public void SwitchToDeadCam(){
    if(mainCam.isActiveAndEnabled){
        deadCam.gameObject.transform.position = mainCam.gameObject.transform.position;
        mainCam.gameObject.SetActive(false);
    }
    deadCam.gameObject.SetActive(true);
}
public void SetCamFollow(CameraType type){
    if(type == CameraType.Main){
        mainCam.Follow = FindObjectOfType<Player>().gameObject.transform;
    }
    if(type == CameraType.Dead){
        deadCam.Follow = FindObjectOfType<Player>().gameObject.transform;
    }
}
}
