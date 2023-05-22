using JetBrains.Annotations;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.UI.SettingsMenu
{
    [RequireComponent(typeof(Button))]
    public class CheckBoxButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textContainer;
        
        [Space(20)]
        [SerializeField] private string onWord = "ON";
        [SerializeField] private string offWord = "OFF";

        private Button button;
        private bool mute;

        public Action<bool> OnChecked;

        public bool Mute => mute;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(SwitchState);
        }

        private void SwitchState() => 
            SetEnabled(!mute);

        public void SetEnabled(bool mute)
        {
            this.mute = mute;
            textContainer.text = mute ? offWord : onWord;
            OnChecked?.Invoke(mute);
        }
    }
}