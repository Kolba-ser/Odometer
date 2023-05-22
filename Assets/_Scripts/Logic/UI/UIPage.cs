using Logic.Sound;
using UnityEngine;

namespace Logic.UI
{
    public abstract class UIPage : MonoBehaviour
    {
        private readonly string soundName = "TurningPage";

        public bool IsOpen { get; protected set; }

        protected virtual void OnOpen() =>
            SoundPlayer.Instance.PlaySound(soundName);

        protected virtual void OnClose() =>
            SoundPlayer.Instance.PlaySound(soundName);
    }
}