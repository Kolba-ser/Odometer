using Infrastructure.Services.Request;
using UnityEngine;

namespace Logic.UI.Odometer
{
    public class OdometerPresenter : MonoBehaviour
    {
        [SerializeField] private OdometerView odometerView;

        private IOdometerListenerService odometerListener;

        public void Construct(IOdometerListenerService odometerListener)
        {
            this.odometerListener = odometerListener;
            this.odometerListener.OnValueRecieved += UpdateValue;
        }

        private void OnDestroy() => 
            odometerListener.OnValueRecieved -= UpdateValue;

        private void UpdateValue(float value) => 
            odometerView.SetValue(value.ToString());
    }
}