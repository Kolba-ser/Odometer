using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

namespace Logic.Sound
{
    public class SoundConfigurator : Singleton<SoundConfigurator>
    {
        [SerializeField] private AudioSource soundsSource;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioMixerGroup master;

        private PrefsValue<bool> isSoundMuted = new PrefsValue<bool>("IsSoundMuted", false);
        private PrefsValue<bool> isMusicMuted = new PrefsValue<bool>("IsMusicMuted", false);
        private PrefsValue<float> savedVolume = new PrefsValue<float>("MasterMixerVolume", 0.5f);
        
        public bool IsSoundMuted => isSoundMuted.Value;
        public bool IsMusicMuted => isMusicMuted.Value;
        public float ActualVolume => savedVolume.Value;

        private void Awake()
        {
            SetVolume(savedVolume.Value);
            SetEnabledSounds(isSoundMuted.Value);
            SetEnabledMusic(isMusicMuted.Value);
        }

        public void SetVolume(float volume)
        {
            savedVolume.Value = volume;
            master.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, volume));
        }

        public void SetEnabledSounds(bool enabled)
        {
            isSoundMuted.Value = enabled;
            soundsSource.mute = enabled;
        }

        public void SetEnabledMusic(bool enabled)
        {
            isMusicMuted.Value = enabled;
            musicSource.mute = enabled;
        }
    }
}