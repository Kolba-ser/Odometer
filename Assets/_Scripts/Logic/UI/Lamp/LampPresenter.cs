using Infrastructure.Services.Request;
using UnityEngine;

namespace Logic.UI.Lamp
{
    public class LampPresenter : MonoBehaviour
    {
        [SerializeField] private LampView lampView;

        private IOdometerListenerService odometerListener;

        public void Construct(IOdometerListenerService odometerListener)
        {
            this.odometerListener = odometerListener;
            this.odometerListener.OnStatusReceived += TryChangeLampState;
        }

        private void OnDestroy()
        {
            odometerListener.OnStatusReceived -= TryChangeLampState;
        }

        private void TryChangeLampState(bool status)
        {
            if(status != lampView.State)
            {
                lampView.SwitchState(status);
            }
        }
    }
}