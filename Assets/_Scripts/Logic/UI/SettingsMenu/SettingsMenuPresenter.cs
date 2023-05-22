using UnityEngine;
using UnityEngine.UI;

namespace Logic.UI.SettingsMenu
{
    public class SettingsMenuPresenter : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private SettingsMenuView settingsView;

        private void Awake()
        {
            button.onClick.AddListener(settingsView.SwitchState);
        }
    }
}
