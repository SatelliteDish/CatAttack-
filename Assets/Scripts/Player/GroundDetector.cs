using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Detector;

public class GroundDetector<T> : MonoBehaviour
{
    [SerializeField]DetectorType[] type;
    Dictionary<string,bool> detectorTypes = new Dictionary<string, bool>();
    bool isTouching = true;
    BoxCollider2D groundDetector;
    
    void Start() {
        string _type = "";
        for(int i = 0; i < type.Length(); i++) {
            _type = DetectorTypeToString(types[i]);
            if(detectorTypes.ContainsKey(_type)) {
                detectorTypes.Add(_type, false);
            }
            else  {
                Debug.LogWarning("Duplicate DetectorType entered");
            }
        }
    }

    string DetectorTypeToString(DetectorType type) {
        switch (type) {
                case DetectorType.Ground:
                    return "Ground";
                case DetectorType.Hazard:
                    return "Hazard";
            }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(detectorTypes.ContainsKey(other.tag)) {
            detectorTypes[other.tag] = true;
        }    
    }

    void OnTriggerExit2D(Collider2D other) {
        if(detectorTypes.ContainsKey(other.tag)) {
            detectorTypes[other.tag] = false;
        }  
    }

    public bool IsTouching(DetectorType _type) {
        string typeString = DetectorTypeToString(_type);
        if(!detectorTypes.ContainsKey(typeString)) {
            Debug.LogError("Error, invalid detector, tried " + typeString);
            return false;
        }
        return detectorTypes[typeString];
    }
}
