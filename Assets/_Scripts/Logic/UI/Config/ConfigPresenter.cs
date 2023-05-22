using Infrastructure.Entities.Config;
using Infrastructure.Services;
using Infrastructure.Services.Progress;
using UnityEngine;

namespace Logic.UI.ConfigUI
{
    public class ConfigPresenter : MonoBehaviour
    {
        [SerializeField] private ConfigView configView;

        private void Start()
        {
            Config config = AllServices.Container.GetSingle<IPersistentConfigService>().Config;
            configView.SetAddress(config.IpAddres);
            configView.SetPort(config.Port.ToString());
        }
    }
}