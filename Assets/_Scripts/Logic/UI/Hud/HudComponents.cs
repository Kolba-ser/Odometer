using Infrastructure.Services;
using Infrastructure.Services.Request;
using Logic.UI.Lamp;
using Logic.UI.Notificatios;
using Logic.UI.Odometer;
using UnityEngine;

namespace Logic.UI.Hud
{
    public class HudComponents : MonoBehaviour
    {
        [SerializeField] private LampPresenter lamp;
        [SerializeField] private OdometerPresenter odometer;
        [SerializeField] private LostConnectionNotificationHandler connectionNotificationHandler;

        public void Construct(AllServices allServices)
        {
            lamp.Construct(allServices.GetSingle<IOdometerListenerService>());
            odometer.Construct(allServices.GetSingle<IOdometerListenerService>());
            connectionNotificationHandler.Construct(allServices.GetSingle<IOdometerListenerService>());
        }
    }
}