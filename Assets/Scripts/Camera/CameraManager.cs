/****************************************************************\
|This class acts as a way of controlling multiple cameras at once|
|- You access the files by an ID set on Camera creation          |
|- ID is not case sensitive                                      |
|- If you enter an invalid ID, you will get an ArgumentException |
\****************************************************************/

using System;
using System.Diagnostics;
using UnityEngine;
public class CameraManager : MonoBehaviour {
    [SerializeField]CameraConfig[] cameras;
    CameraConfig currentCam;

    void Start(){
        //TODO: Take this out of CameraManager, what if no camera ID main?
        try{
            SwitchCam("main");
        }
        catch(ArgumentException error) {
            Debug.Log(error.Message);
        }
    }
    //Enables camera with specified ID, all other cameras disabled
    //All other cameras disabled even if an exception is thrown 
    //TODO: maybe fix that?
    public void SwitchCam(string id) {
        id = id.ToLower();
        bool foundCam = false;
        for(int i = 0; i < cameras.Length; i++) {
            if(cameras[i].Name().ToLower() == id) {
                currentCam = cameras[i];
                cameras[i].SetActive(true);
                foundCam = true;
            }
            else {
                cameras[i].SetActive(false);
            }
        }
        if(!foundCam) {
            HandleIDError(id);
        }
    }
    //Sets the follow for the specified cam to the specified transform.
    public void SetCamFollow(string id, Transform transform){
        id = id.ToLower();
        bool foundCam = false;
        if(id == "current") {
            currentCam.SetFollow(transform);
            foundCam = true;
        }
        else {
            for(int i = 0; i < cameras.Length; i++) {
                if(cameras[i].Name().ToLower() == id) {
                    foundCam = true;
                    cameras[i].SetFollow(transform);
                    break;
                }
                else if(id == "all") {
                    cameras[i].SetFollow(transform);
                    foundCam = true;
                }
            }
        }
        if(!foundCam) {
            HandleIDError(id);
        }
    }
    private bool HandleIDError(string id) {
        throw new ArgumentException(String.Format("{0} is not a valid ID", id), "id");
    }
}