using Infrastructure.Entities.Config;
using Infrastructure.Entities.Server;
using Infrastructure.Services;
using Infrastructure.Services.Progress;
using Infrastructure.Services.Request;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.UI.SettingsMenu
{
    public class ReconnectButton : MonoBehaviour
    {
        [SerializeField] private Button reconnectButton;
        [Space(20)]
        [SerializeField] private TMP_InputField addressInputField;
        [SerializeField] private TMP_InputField portInputField;

        private void Awake() => 
            reconnectButton.onClick.AddListener(Reconnect);

        private async void Reconnect()
        {
            Config config = AllServices.Container.GetSingle<IPersistentConfigService>().Config;
            config.IpAddres = addressInputField.text;
            config.Port = int.Parse(portInputField.text);
            
            string url = $"ws://{config.IpAddres}:{config.Port}/ws";
            AllServices.Container.GetSingle<IOdometerListenerService>().Stop();
            await AllServices.Container.GetSingle<IOdometerServerConnetion>().Connect(url);
            AllServices.Container.GetSingle<IOdometerListenerService>().Track();
        }
    }
}