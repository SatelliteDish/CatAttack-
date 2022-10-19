/*****************************************************************************\
|This class wraps a Cinemachine Camera                                        |
|TODO: Change the name, Camera was taken (unsurprisingly) and this isn't ideal|
\*****************************************************************************/
using UnityEngine;
using Cinemachine;

public class CameraConfig: MonoBehaviour {
    [SerializeField]private string _name;
    [SerializeField]CinemachineVirtualCamera _camera;

    public string Name() {
        return _name;
    }
    public void SetFollow(Transform transform) {
        _camera.Follow = transform;
    }
    public void SetActive(bool val) {
        _camera.enabled = !_camera.enabled ;
    }
}