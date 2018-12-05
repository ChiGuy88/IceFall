using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;
using TMPro;
using DG.Tweening;

namespace IceFalls {

    public class script_ScoreText : CRYSTAL_Script {

        public string DisplayText;

        public float TweenScoreUp_OffsetY = 2f;

        public float TweenScoreUp_Time = 2f;

        public Ease TweenScoreUp_Ease = Ease.OutBack;

        public float PositionOffsetY = 2f;
        
        private TextMeshPro p_textMeshPro = null;
        private TextMeshPro TextMeshPro {
            get {
                if (this.p_textMeshPro == null) {
                    this.p_textMeshPro = this.GetComponent<TextMeshPro>();
                }
                return this.p_textMeshPro;
            }
        }

        public override void SetDefaultValues() {
            base.SetDefaultValues();

            // Update TextMeshPro script
            this.TextMeshPro.SetText(this.DisplayText);

            // Tween position to offset
            RectTransform rectTransform = this.GetComponent<RectTransform>();
            rectTransform.localPosition = Vector3.up * PositionOffsetY;
            rectTransform.DOLocalMove(rectTransform.localPosition + (Vector3.up * this.TweenScoreUp_OffsetY), this.TweenScoreUp_Time)
                .SetEase(this.TweenScoreUp_Ease);

            // Tween alpha
            Color endCOlor = new Color(this.TextMeshPro.color.r, this.TextMeshPro.color.g, this.TextMeshPro.color.b, 0);
            this.TextMeshPro.DOColor(endCOlor, this.TweenScoreUp_Time);

            Destroy(this.gameObject, this.TweenScoreUp_Time);
        }
    }
}