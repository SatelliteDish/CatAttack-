using System.Diagnostics;
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
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.Clip();
            s.source.pitch = s.Pitch();
            s.source.loop = s.loop;
            if(s.isBGM) {
                s.source.volume = PlayerPrefs.GetFloat("BGMVolume")/ 10;
            }
            if(s.isSFX) {
                s.source.volume = PlayerPrefs.GetFloat("SFXVolume")/ 10;
            }
            if(!s.isBGM && !s.isSFX) {
            s.source.volume = .75f; 
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
            if(s.isBGM) {
                s.source.volume = PlayerPrefs.GetFloat("BGMVolume")/ 10;
            }
            if(s.isSFX) {
                s.source.volume = PlayerPrefs.GetFloat("SFXVolume")/ 10;
            }
            if(!s.isBGM && !s.isSFX) {
                s.source.volume = .75f; 
            }
        }
    }
    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null) {
            SoundNotFound(name);
        }
        s.source.Play();
    }
    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null) {
            SoundNotFound(name);
        }
        s.source.Stop();
        }
        
    public void Pause(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null) {
            SoundNotFound(name);
        }
        s.source.Pause();
    }
    private void SoundNotFound(string name) {
            throw new ArgumentException(String.Format("Sound name {0} not found!",name));
    }
}