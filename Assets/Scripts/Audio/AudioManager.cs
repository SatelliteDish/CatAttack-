using UnityEngine;
using UnityEngine.Audio;
using System;
public class AudioManager : MonoBehaviour
{
    [SerializeField]Sound[] sounds;
    static AudioManager instance;

    void Awake() {
        if(instance == null)
        instance = this;
        else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        if(!PlayerPrefs.HasKey("SFXVolume")) {
            PlayerPrefs.SetFloat("SFXVolume", 1f);
        }
        if(!PlayerPrefs.HasKey("BGMVolume")) {
            PlayerPrefs.SetFloat("BGMVolume", 1f);
        }
        foreach (Sound s in sounds) {
            s.Source(gameObject.AddComponent<AudioSource>());
            s.Source().clip = s.Clip();
            s.Source().pitch = s.Pitch();
            s.Source().loop = s.Loop();
            if(s.IsBGM()) {
                s.Source().volume = PlayerPrefs.GetFloat("BGMVolume")/ 10;
            }
            if(s.IsSfx()) {
                s.Source().volume = PlayerPrefs.GetFloat("SFXVolume")/ 10;
            }
            if(!s.IsBGM() && !s.IsSfx()) {
            s.Source().volume = .75f; 
            }
        }
    }
    void Start() {
        //TODO: Move this somewhere else
        try {
            Play("Music");
        }
        catch(ArgumentException error) {
            Debug.LogError(error.Message);
        }
    }
    public void UpdateVolumeLevels() {
        foreach(Sound s in sounds){
            if(s.IsBGM()) {
                s.Source().volume = PlayerPrefs.GetFloat("BGMVolume")/ 10;
            }
            if(s.IsSfx()) {
                s.Source().volume = PlayerPrefs.GetFloat("SFXVolume")/ 10;
            }
            if(!s.IsBGM() && !s.IsSfx()) {
                s.Source().volume = .75f; 
            }
        }
    }
    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.Name() == name);
        if(s == null) {
            SoundNotFound(name);
        }
        s.Source().Play();
    }
    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.Name() == name);
        if(s == null) {
            SoundNotFound(name);
        }
        s.Source().Stop();
        }
        
    public void Pause(string name) {
        Sound s = Array.Find(sounds, sound => sound.Name() == name);
        if(s == null) {
            SoundNotFound(name);
        }
        s.Source().Pause();
    }
    private void SoundNotFound(string name) {
            throw new ArgumentException(String.Format("Sound name {0} not found!",name));
    }
}