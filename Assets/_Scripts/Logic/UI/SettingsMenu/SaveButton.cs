using Infrastructure.SaveLoad;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.UI.SettingsMenu
{
    [RequireComponent(typeof(Button))]
    public class SaveButton : MonoBehaviour
    {
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(Save);
        }

        private void Save()
        {
            AllServices.Container.GetSingle<ISaveLoadConfigService>().Save();
        }
    }
}