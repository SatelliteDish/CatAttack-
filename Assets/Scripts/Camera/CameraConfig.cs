/*****************************************************************************\
|This class wraps a Cinemachine Camera                                        |
|TODO: Change the name, Camera was taken (unsurprisingly) and this isn't ideal|
\*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraConfig {
    [SerializeField]private string name;
    [SerializeField]ICinemachineCamera camera;

    public string Name() {
        return name;
    }
    public void SetFollow(Transform transform) {
        camera.Follow = transform;
    }
    public void SetActive(bool val) {
        this.SetActive(val);
    }
}
