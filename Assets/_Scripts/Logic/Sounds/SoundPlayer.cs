using Infrastructure.Services;
using Infrastructure.Services.Providers.Assets;
using Logic.StaticData;
using System;
using UnityEngine;

namespace Logic.Sound
{
    public class SoundPlayer : Singleton<SoundPlayer>
    {

        [SerializeField] private AudioSource soundsSource;
        [SerializeField] private AudioSource musicSource;

        private SoundsContainer soundsContainer;

        private void Awake()
        {
            soundsContainer = AllServices.Container.GetSingle<IAssetsProvider>().Load<SoundsContainer>(AssetsPath.SOUNDS_PATH);
        }

        public void PlaySound(string soundName)
        {
            if(TryFindClip(soundName, out AudioClip clip))
            {
                soundsSource.PlayOneShot(clip);
            }
        }

        public void StopSound() => 
            soundsSource.Stop();

        public void PlayMusic(string musicName)
        {
            if (TryFindClip(musicName, out AudioClip clip))
            {
                musicSource.clip = clip;
                musicSource.Play();
            }
        }
        public void StopMusic() => 
            musicSource.Stop();

        private bool TryFindClip(string name, out AudioClip clip)
        {
            clip = null;

            foreach (var sound in soundsContainer.Sounds)
            {
                if (sound.Name == name)
                {
                    clip = sound.Clip;
                    break;
                }
            }

            return clip != null;
        }
    }
}