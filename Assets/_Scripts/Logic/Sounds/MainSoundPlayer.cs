using Logic.Sound;
using UnityEngine;

namespace Logic.SettingsMenu
{
    public class MainSoundPlayer : MonoBehaviour
    {
        private readonly string mainMusicName = "Main";

        private void Start() => SoundPlayer.Instance.PlayMusic(mainMusicName);
    }
}