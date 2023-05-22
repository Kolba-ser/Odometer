using Logic.Sound;
using Logic.UI.SettingsMenu;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.SettingsMenu
{
    public class SoundSettingsConfigurator : MonoBehaviour
    {
        [SerializeField] private CheckBoxButton soundsButton;
        [SerializeField] private CheckBoxButton musicButton;
        [SerializeField] private Slider volumeSlider;

        private void Awake()
        {
            soundsButton.OnChecked += SetEnabledSounds;
            musicButton.OnChecked += SetEnabledMusic;
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }

        private void Start()
        {
            soundsButton.SetEnabled(SoundConfigurator.Instance.IsSoundMuted);
            musicButton.SetEnabled(SoundConfigurator.Instance.IsMusicMuted);
            volumeSlider.value = SoundConfigurator.Instance.ActualVolume;
        }

        private void OnDestroy()
        {
            soundsButton.OnChecked -= SetEnabledSounds;
            musicButton.OnChecked -= SetEnabledMusic;
            volumeSlider.onValueChanged.RemoveListener(ChangeVolume);
        }

        private void SetEnabledSounds(bool enabled) =>
            SoundConfigurator.Instance.SetEnabledSounds(enabled);

        private void SetEnabledMusic(bool enabled) =>
            SoundConfigurator.Instance.SetEnabledMusic(enabled);

        private void ChangeVolume(float value) =>
            SoundConfigurator.Instance.SetVolume(value);
    }
}