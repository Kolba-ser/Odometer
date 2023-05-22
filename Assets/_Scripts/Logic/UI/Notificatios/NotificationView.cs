using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Logic.UI.Notificatios
{
    public class NotificationView : UIPage
    {
        [SerializeField] private TextMeshProUGUI textContainer;
        [SerializeField] private RectTransform content;

        [Space(20)]
        [SerializeField] private float showOffsetY;
        [SerializeField] private float hideOffsetY;
        [SerializeField] private float appeareDuration;

        [Space(20)]
        [SerializeField] private float writingStartScale;
        [SerializeField] private float writingEndScale;
        [SerializeField] private float writingScaleDuration;
        [SerializeField] private float writingAlphaDuration;
        [SerializeField] private float writingInterval;
        [SerializeField] private Ease ease;

        private IEnumerator animation;
        private DG.Tweening.Sequence sequence;
        private DOTweenTMPAnimator animator;

        public void ShowNotifiation(string message)
        {
            textContainer.text = message;
            content.gameObject.SetActive(true);
            textContainer.gameObject.SetActive(false);
            ShiftContent(showOffsetY, Animate);

            IsOpen = true;
            OnOpen();
            
            void Animate()
            {
                textContainer.gameObject.SetActive(true);
                animation = AnimateWriting(textContainer);
                StartCoroutine(animation);
            }
        }

        public void HideNotifiation()
        {
            Action callback = () => content.gameObject.SetActive(false);
            
            IsOpen = false;

            ShiftContent(hideOffsetY, callback);
            OnClose();
        }

        private void ShiftContent(float toPosY, Action callback)
        {
            content.DOKill();
            content.DOAnchorPosY(toPosY, appeareDuration).SetEase(ease).OnComplete(() => callback());
        }

        private IEnumerator AnimateWriting(TextMeshProUGUI textMeshProUGUI)
        {
            textMeshProUGUI.ForceMeshUpdate();
            sequence = DOTween.Sequence();
            animator = new DOTweenTMPAnimator(textMeshProUGUI);

            TMP_TextInfo textInfo = textMeshProUGUI.textInfo;

            Color32 c0 = textMeshProUGUI.color;
            c0.a = 0;
            textMeshProUGUI.color = c0;
            for (int i = 0; i < textMeshProUGUI.textInfo.characterCount; i++)
                animator.DOScaleChar(i, writingStartScale, 0);

            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < animator.textInfo.characterCount; ++i)
            {
                if (!animator.textInfo.characterInfo[i].isVisible)
                    continue;

                sequence.Join(animator.DOScaleChar(i, writingEndScale, writingScaleDuration));
                sequence.Join(animator.DOFadeChar(i, 1, writingAlphaDuration));
                yield return new WaitForSeconds(writingInterval);
            }
            KillAnimation();
        }

        private void KillAnimation()
        {
            StopCoroutine(animation);
            animator?.Dispose();
            sequence?.Kill();
            sequence = null;
        }
    }
}