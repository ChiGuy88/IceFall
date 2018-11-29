using UnityEngine;
using DG.Tweening;

namespace IceFalls {

    public class CanvasGroupAlphaTween : Tween {

        public float StartAlpha;

        public float EndAlpha;

        public bool UseCurrentAlphaToStart;

        protected override void _PlayTween() {
            base._PlayTween();

            CanvasGroup canvasGroup = this.ParentObject.GetComponent<CanvasGroup>();

            DOTween.Kill(this.ParentObject);
            DOTween.To(
                () => UseCurrentAlphaToStart ? canvasGroup.alpha : this.StartAlpha,
                (float value) => {
                    canvasGroup.alpha = value;
                },
                this.EndAlpha,
                this.TweenTimeInSec
            )
            .SetId(this.ParentObject)
            .SetEase(this.TweenEase);
        }

        protected override void _StopTween() {
            base._StopTween();
            
            DOTween.Kill(this.ParentObject);
        }
    }
}