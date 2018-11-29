using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace IceFalls {

    [Serializable]
    public class TransformScaleTween : Tween, ITweenScript {

        public Vector3 StartScale;

        public Vector3 EndScale;

        override protected void _PlayTween() {
            base._PlayTween();

            DOTween.To(
                () => this.StartScale,
                (Vector3 value) => {
                    this.ParentObject.transform.localScale = value;
                },
                this.EndScale,
                this.TweenTimeInSec
            )
            .SetEase(this.TweenEase)
            .OnComplete(this._OnTweenComplete);
        }
    }
}