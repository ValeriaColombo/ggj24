using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviourWithContext
{
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider VoiceSlider;

    public void Show()
    {
        MusicSlider.value = MySoundManager.MusicVolume;
        SFXSlider.value = MySoundManager.SFXVolume;
        VoiceSlider.value = MySoundManager.VoiceVolume;

        MusicSlider.onValueChanged.AddListener(OnMusicSliderValueChange);
        SFXSlider.onValueChanged.AddListener(OnSFXSliderValueChange);
        VoiceSlider.onValueChanged.AddListener(OnVoiceSliderValueChange);
    }

    public void Hide()
    {
        MusicSlider.onValueChanged.RemoveAllListeners();
        SFXSlider.onValueChanged.RemoveAllListeners();
        VoiceSlider.onValueChanged.RemoveAllListeners();
    }

    private void OnMusicSliderValueChange(float newValue)
    {
        MySoundManager.ChangeMusicVolume(MusicSlider.value);
    }

    private void OnSFXSliderValueChange(float newValue)
    {
        MySoundManager.ChangeSFXVolume(SFXSlider.value);
    }

    private void OnVoiceSliderValueChange(float newValue)
    {
        MySoundManager.ChangeVoiceVolume(VoiceSlider.value);
    }
}
