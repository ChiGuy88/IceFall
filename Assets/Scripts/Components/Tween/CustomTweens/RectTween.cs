using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using CRYSTAL;

namespace IceFalls {
    
    [Serializable]
    public class RectTween : Tween, ITweenScript {

        public Rect StartRect = Rect.zero;

        public Rect EndRect = Rect.zero;

        override protected void _PlayTween() {
            base._PlayTween();

            RectTransform rectTransform = this.ParentObject.GetComponent<RectTransform>();

            DOTween.To(
                () => this.StartRect,
                (Rect value) => {
                    if (rectTransform == null) return;
                    rectTransform.localPosition = new Vector3(value.x, value.y);
                    rectTransform.sizeDelta = new Vector2(value.width, value.height);
                },
                this.EndRect,
                this.TweenTimeInSec
            )
            .SetEase(this.TweenEase)
            .OnComplete(this._OnTweenComplete);
        }
    }
}