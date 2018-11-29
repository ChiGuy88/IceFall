using UnityEngine;
using System;
using CRYSTAL;

namespace IceFalls {

    public enum ETweenType {
        NONE,
        Rect2D,
        TransformPosition,
        TransformRotation,
        TransformScale,
        CanvasGroupAlpha
    }

    [Serializable]
    public class script_Tween : CRYSTAL_Script {

        public ETweenType TweenType = ETweenType.NONE;

        public Tween Tween;

        public override void Step() {
            base.Step();

            if (this.Tween == null) {
                if (this.TweenType != ETweenType.NONE) {
                    CONSOLE.Warn("Why is tween value null?", this.TweenType, this.Tween);
                }
                return;
            }

            this.Tween.PlayTween();
        }

        public virtual void ChangeTweenType() {

            CONSOLE.Log("ChangeTweenType(" + this.TweenType + ")");

            switch (this.TweenType) {
                case ETweenType.Rect2D:
                    this.Tween = ScriptableObject.CreateInstance<RectTween>();
                    break;
                case ETweenType.TransformPosition:
                    this.Tween = ScriptableObject.CreateInstance<TransformPositionTween>();
                    break;
                case ETweenType.TransformRotation:
                    this.Tween = ScriptableObject.CreateInstance<TransformRotationTween>();
                    break;
                case ETweenType.TransformScale:
                    this.Tween = ScriptableObject.CreateInstance<TransformScaleTween>();
                    break;
                case ETweenType.CanvasGroupAlpha:
                    this.Tween = ScriptableObject.CreateInstance<CanvasGroupAlphaTween>();
                    break;
                case ETweenType.NONE:
                default:
                    this.Tween = null;
                    return;
            }

            this.Tween.Init(this.gameObject);
        }

        public T GetTween<T>() { return (T)Convert.ChangeType(this.Tween, typeof(T)); }
    }
}