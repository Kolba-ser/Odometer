using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Logic.UI.Odometer
{
    public class OdometerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textContainer;

        [Space(20)]
        [SerializeField] private float switchDuration;

        [Space(20)]
        [SerializeField] private float writingStartScale;

        [SerializeField] private float writingEndScale;
        [SerializeField] private float writingScaleDuration;
        [SerializeField] private float writingAlphaDuration;
        [SerializeField] private float writingInterval;
        [SerializeField] private Ease ease;

        private DG.Tweening.Sequence sequence;
        private DOTweenTMPAnimator animator;
        private IEnumerator animation;

        public void SetValue(string value)
        {
            textContainer.text = value;
            animation = AnimateWriting(textContainer);
            StartCoroutine(animation);
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