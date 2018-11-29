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

        protected CRYSTAL_Rect2D script_Rect2D {
            get {
                return this.ParentObject.GetComponent<CRYSTAL_Rect2D>();
            }
        }

        override protected void _PlayTween() {
            base._PlayTween();

            CRYSTAL_Rect2D script = this.script_Rect2D;
            script.UpdatePositionRect(this.StartRect);

            DOTween.To(
                () => this.StartRect,
                (Rect _updatedRect) => {
                    script.UpdatePositionRect(_updatedRect);
                },
                this.EndRect,
                this.TweenTimeInSec
            )
            .SetEase(this.TweenEase)
            .OnComplete(this._OnTweenComplete);
        }
    }
}