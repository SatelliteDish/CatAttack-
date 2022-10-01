using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class OptionsMenu : MonoBehaviour {
    enum SliderType{
        BGM,
        SFX
    };
    [SerializeField]Slider sfxSlider;
    [SerializeField]Slider bgmSlider;
    AudioManager audioManager;
    [SerializeField]Image[] sFXTicks;
    [SerializeField]Image[] bGMPawprints;
    [SerializeField]Image[] bGMTicks;
    [SerializeField]Image[] sFXPawprints;
    DependencyManager<OptionsMenu> dependencyManager;
    [SerializeField]TextMeshProUGUI musicVolumeText;
    [SerializeField]TextMeshProUGUI sFXVolumeText;
    float sfxVolume;
    float bGMVolume;
    void Awake(){
        SetReferences();
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        UpdateSlider(SliderType.BGM);
        UpdateSlider(SliderType.SFX);
        for(int i = 0; i < sFXTicks.Length; i++){
            sFXTicks[i].transform.localPosition = new Vector2 ((i * 100) - 500, sFXTicks[i].transform.localPosition.y);
        }
        for(int i = 0; i < bGMTicks.Length; i++){
            bGMTicks[i].transform.localPosition = new Vector2 ((i * 100) - 500, bGMTicks[i].transform.localPosition.y);
        }
    }
    void SetReferences(){
        dependencyManager = FindObjectOfType<DependencyManager<OptionsMenu>>();
        audioManager = dependencyManager.GetManagersRepo().GetAudioManager();
    }
    void Update(){
        if(sfxSlider.value != sfxVolume){
            UpdateSlider(SliderType.SFX);
        }
        if(bgmSlider.value != bGMVolume){
            UpdateSlider(SliderType.BGM);
        }
    }
    void UpdateSlider(SliderType type){
        if(type == SliderType.BGM){
            bGMVolume = bgmSlider.value;
            PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value);
            audioManager.UpdateVolumeLevels();
            for(int i = 0; i < bGMPawprints.Length; i++){
                bGMPawprints[i].gameObject.SetActive(false);
            }
            for(int i = 0; i < bgmSlider.value; i++){
                bGMPawprints[i].gameObject.SetActive(true);
            }
        }
        if(type == SliderType.SFX){
            sfxVolume = sfxSlider.value;
            PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
            audioManager.UpdateVolumeLevels();
            for(int i = 0; i < sFXPawprints.Length; i++){
                sFXPawprints[i].gameObject.SetActive(false);
            }
            for(int i = 0; i < sfxSlider.value; i++){
                sFXPawprints[i].gameObject.SetActive(true);
            }
        }
    }
}