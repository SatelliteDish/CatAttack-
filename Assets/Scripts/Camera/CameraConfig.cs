using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraConfig {
    [SerializeField]private string name;
    [SerializeField]CinemachineVirtualCamera camera;

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
