using DG.Tweening;
using UnityEngine;
using System;
using CRYSTAL;

namespace IceFalls {

    public delegate void OnTweenCompleteDelegate(Tween tween);

    [Serializable]
    public class Tween : ScriptableObject, ITweenScript {

        [SerializeField]
        protected GameObject ParentObject;

        [SerializeField]
        private string _GUID;

        public string GUID {
            get {
                return _GUID;
            }
            private set {
                _GUID = value;
            }
        }

        public string TweenDisplayName;

        public float TweenStartDelayInSec;

        public float TweenTimeInSec;

        public bool ForcePlayTween;

        public bool ForceReplayTween;

        public Ease TweenEase;

        private bool _IsTweenPlaying;

        private OnTweenCompleteDelegate _OnTweenCompleteDelegate;

        public void OnEnable() {
            //hideFlags = HideFlags.HideAndDontSave;
            if (this.GUID == null || this.GUID.Equals(string.Empty)) {
                this.GUID = Guid.NewGuid().ToString();
            }
        }
        ///
        
        ///
        public void Init(
            GameObject _ParentObject,
            bool _ForcePlay = true,
            bool _ForceRepeat = false,
            float _TweenTimeInSeconds = 0.5f,
            float _TweenStartDelayInMS = 0f,
            Ease _Ease = Ease.Linear
        ) {
            this.ParentObject = _ParentObject;
            this.ForcePlayTween = _ForcePlay;
            this.ForceReplayTween = _ForceRepeat;
            this.TweenTimeInSec = _TweenTimeInSeconds;
            this.TweenStartDelayInSec = _TweenStartDelayInMS;
            this.TweenEase = _Ease;

            this.SetDefaultValues();
        }
        ///

        ///
        protected virtual void SetDefaultValues() { }
        ///

        public void OnComplete(OnTweenCompleteDelegate _Callback) {
            this._OnTweenCompleteDelegate = _Callback;
        }

        ///
        public void PlayTween() {
            if (this._IsTweenPlaying == false && this.ForcePlayTween) {
                this._IsTweenPlaying = true;
                this.ForcePlayTween = this.ForceReplayTween;

                if (this.TweenStartDelayInSec > 0) {

                    TimerInfo tInfo = TimerSystem.CreateTimer(this.GUID, this.TweenStartDelayInSec);
                    tInfo.Watch((string _key, TimerInfo _info) => {
                        this._PlayTween();
                    });
                    tInfo.Start();
                    return;
                }

                this._PlayTween();
            }
        }
        ///

        ///
        protected virtual void _PlayTween() {}
        ///

        ///
        public void StopTween() {
            if (this._IsTweenPlaying) {
                this._IsTweenPlaying = false;
                TimerSystem.RemoveTimer(this.GUID);
                this._StopTween();
            }
        }
        ///

        ///
        protected virtual void _StopTween() {}
        ///
        
        ///
        protected void _OnTweenComplete() {
            this.StopTween();

            if (this._OnTweenCompleteDelegate != null) {
                this._OnTweenCompleteDelegate(this);
            }

            this.PlayTween();
        }
        ///
    }
}