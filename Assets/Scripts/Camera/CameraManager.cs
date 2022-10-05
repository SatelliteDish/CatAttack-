using UnityEngine;
public class CameraManager : MonoBehaviour {
    [SerializeField]CameraConfig[] cameras;
    CameraConfig currentCam;

    void Start(){
        SwitchCam("main");
    }
    public void SwitchCam(string id) {
        for(int i = 0; i < cameras.Length; i++) {
            if(cameras[i].Name() == id) {
                currentCam = cameras[i];
                cameras[i].SetActive(true);
            }
            else {
                cameras[i].SetActive(false);
            }
        }
    }
    public void SetCamFollow(string id, Transform transform){
        if(id == "current") {
            currentCam.SetFollow(transform);
        }
        else {
            for(int i = 0; i < cameras.Length; i++) {
                if(cameras[i].Name() == id) {
                    cameras[i].SetFollow(transform);
                    break;
                }
                else if(id == "all") {
                    cameras[i].SetFollow(transform);
                }
            }
        }
    }
}