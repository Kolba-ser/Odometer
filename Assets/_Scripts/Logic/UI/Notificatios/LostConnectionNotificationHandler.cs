using Infrastructure.Services.Request;
using System;
using UnityEngine;

namespace Logic.UI.Notificatios
{
    public class LostConnectionNotificationHandler : MonoBehaviour
    {
        [TextArea]
        [SerializeField] private string message;

        private IOdometerListenerService odometerListener;

        public void Construct(IOdometerListenerService odometerListener)
        {
            this.odometerListener = odometerListener;
            odometerListener.OnStatusReceived += HandleStatus;
        }

        private void OnDestroy()
        {
            odometerListener.OnStatusReceived -= HandleStatus;
        }

        private void HandleStatus(bool status)
        {
            if (!status)
                NotificationPresenter.Instance.Show(message);
            else
                NotificationPresenter.Instance.Hide();
        }
    }
}