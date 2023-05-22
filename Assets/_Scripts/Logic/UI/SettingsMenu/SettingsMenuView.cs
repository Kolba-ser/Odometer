using DG.Tweening;
using UnityEngine;

namespace Logic.UI.SettingsMenu
{
    public class SettingsMenuView : UIPage
    {
        [SerializeField] private RectTransform menu;
        [Space(20)]
        [SerializeField] private float showOffsetX;
        [SerializeField] private float hideOffsetX;
        [SerializeField] private float switchDuration;
        [SerializeField] private Ease ease;

        private bool isOpen;

        public void SwitchState()
        {
            if (isOpen)
                Hide();
            else
                Show();
        }

        private void Show()
        {
            isOpen = true;
            menu.DOKill();

            menu.gameObject.SetActive(true);
            menu.DOAnchorPosX(showOffsetX, switchDuration).SetEase(ease);
            OnOpen();
        }

        private void Hide()
        {
            isOpen = false;
            menu.DOKill();

            menu.DOAnchorPosX(hideOffsetX, switchDuration)
                .OnComplete(() => menu.gameObject.SetActive(false)).SetEase(ease);
            OnClose();
        }
    }
}