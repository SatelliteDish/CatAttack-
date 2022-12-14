using UnityEngine.Audio;
using UnityEngine;
[System.Serializable]
public class Sound {
    [SerializeField] string name;
    [SerializeField] AudioClip clip;
    [Range(0f, 10f)]
    [SerializeField] float volume;
    [Range(.1f, 3f)]
    [SerializeField] float pitch;
    [SerializeField] bool loop;
    [SerializeField] bool isSFX;
    [SerializeField] bool isBGM;
    [HideInInspector]
    [SerializeField] AudioSource source;
    public string Name() {
        return name;
    }
    public AudioClip Clip() {
        return clip;
    }
    public float Volume() {
        return volume;
    }
    public float Pitch() {
        return pitch;
    }
    public bool Loop() {
        return loop;
    }
    public bool IsSfx() {
        return isSFX;
    }
    public bool IsBGM() {
        return isBGM;
    }
    public AudioSource Source() {
        return source;
    }
    public void Source(AudioSource _source) {
        source = _source;
    }
}
