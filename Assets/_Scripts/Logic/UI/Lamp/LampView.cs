using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.UI.Lamp
{
    public class LampView : MonoBehaviour
    {

        [SerializeField] private RectTransform content;

        [Space(20)]
        [SerializeField] private float switchDuration;
        [SerializeField] private float offsetY;
        [SerializeField] private Ease ease;

        private Sequence sequence;

        private bool currentState = false;

        public bool State => currentState;

        private void Awake()
        {
            sequence = DOTween.Sequence();
        }

        public void SwitchState(bool state)
        {
            currentState = state;
            
            if (state)
                EnableLamp();
            else
                DisableLamp();
        }
        [ContextMenu("Switch")]
        private void TestSwitchState()
        {
            SwitchState(!currentState);
        }

        private void EnableLamp() => 
            SwitchSprite(offsetY);

        private void DisableLamp() => 
            SwitchSprite(0);

        private void SwitchSprite(float toPositionY)
        {
            sequence?.Kill();
            sequence
                .Append(content.DOAnchorPosY(toPositionY, switchDuration).SetEase(ease));

        }
    }
}