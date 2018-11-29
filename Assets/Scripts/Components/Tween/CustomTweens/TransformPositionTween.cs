using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using CRYSTAL;

namespace IceFalls {

    [Serializable]
    public class TransformPositionTween : Tween, ITweenScript {

        public Vector3 StartPosition;

        public Vector3 EndPosition;

        override protected void _PlayTween() {
            base._PlayTween();
            
            DOTween.To(
                () => this.StartPosition,
                (Vector3 value) => {
                    this.ParentObject.transform.position = value;
                },
                this.EndPosition,
                this.TweenTimeInSec
            )
            .SetEase(this.TweenEase)
            .OnComplete(this._OnTweenComplete);
        }
    }
}