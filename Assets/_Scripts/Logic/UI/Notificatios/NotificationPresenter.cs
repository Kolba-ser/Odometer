using UniRx;
using UnityEngine;

namespace Logic.UI.Notificatios
{
    public class NotificationPresenter : Singleton<NotificationPresenter>
    {
        [SerializeField] private NotificationView notificationView;

        public void Show(string message)
        {
            if(!notificationView.IsOpen)
                notificationView.ShowNotifiation(message);
        }

        public void Hide()
        {
            if (notificationView.IsOpen)
                notificationView.HideNotifiation();
        }
    }
}