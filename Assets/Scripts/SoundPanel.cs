using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundPanel : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private Slider _musicVolume;
    [SerializeField] private Slider _soundVolume;    
    [SerializeField] private Toggle _muteMusic;
    [SerializeField] private Settings _settings;

    private void OnEnable()
    {
        Time.timeScale = 0f;

        _musicVolume.onValueChanged.AddListener(ChangeVolume);
        _soundVolume.onValueChanged.AddListener(SoundVolume);
        _muteMusic.onValueChanged.AddListener(ToggleMusic);
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;

        _musicVolume.onValueChanged.RemoveListener(ChangeVolume);
        _soundVolume.onValueChanged.RemoveListener(SoundVolume);
        _muteMusic.onValueChanged.RemoveListener(ToggleMusic);
    }

    public void ToggleMusic(bool value)
    {
        if (value)
            _audioMixerGroup.audioMixer.SetFloat("MusicVolume", 0);
        else
            _audioMixerGroup.audioMixer.SetFloat("MusicVolume", -80);
    }

    public void ChangeVolume(float volume)
    {
        _audioMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SoundVolume(float volume)
    {
        _audioMixerGroup.audioMixer.SetFloat("EffectsVolume", Mathf.Log10(volume) * 20);
        _audioMixerGroup.audioMixer.SetFloat("UIVolume", Mathf.Log10(volume) * 20);
    }
}