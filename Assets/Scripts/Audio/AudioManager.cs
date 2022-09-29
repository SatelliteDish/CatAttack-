using UnityEngine;
using UnityEngine.Audio;
using System;
public class AudioManager : MonoBehaviour
{
    [SerializeField]Sound[] sounds;
    [SerializeField] static AudioManager instance;

    void Awake() {
        if(instance == null)
        instance = this;
        else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        if(!PlayerPrefs.HasKey("SFXVolume")){
            PlayerPrefs.SetFloat("SFXVolume", 1f);
        }
        if(!PlayerPrefs.HasKey("BGMVolume")){
            PlayerPrefs.SetFloat("BGMVolume", 1f);
        }
        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            if(s.isBGM)
            {s.source.volume = PlayerPrefs.GetFloat("BGMVolume")/ 10;}
            if(s.isSFX)
            {s.source.volume = PlayerPrefs.GetFloat("SFXVolume")/ 10;}
            if(!s.isBGM && !s.isSFX){
            s.source.volume = .75f; }}}
    void Start(){
         Play("Music");}
    public void UpdateVolumeLevels(){
        foreach(Sound s in sounds){
            if(s.isBGM)
            {s.source.volume = PlayerPrefs.GetFloat("BGMVolume")/ 10;}
            if(s.isSFX)
            {s.source.volume = PlayerPrefs.GetFloat("SFXVolume")/ 10;}
            if(!s.isBGM && !s.isSFX){
            s.source.volume = .75f; }}}
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            Debug.LogWarning("Sound: " + name + "not found!");
        }
        s.source.Play();
        }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){Debug.LogWarning("Sound: " + name + "not found!");}
        s.source.Stop();
        }
        
    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){Debug.LogWarning("Sound: " + name + "not found!");}
        s.source.Pause();
    }
}