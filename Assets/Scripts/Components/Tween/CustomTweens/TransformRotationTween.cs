using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace IceFalls {

    public class TransformRotationTween : Tween, ITweenScript {

        public Vector3 StartRotation;

        public Vector3 EndRotation;

        override protected void _PlayTween() {
            base._PlayTween();

            this.ParentObject.transform.DORotate(this.EndRotation, this.TweenTimeInSec)
                .SetId(this.ParentObject)
                .SetEase(this.TweenEase)
                .OnComplete(this._OnTweenComplete);
        }
    }
}