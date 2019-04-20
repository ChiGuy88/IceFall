using UnityEngine;
using CRYSTAL;
using DG.Tweening;

namespace IceFalls {

    public class script_IceBlock : CRYSTAL_Script {

        // Public

        public readonly string GUID = System.Guid.NewGuid().ToString();

        public GameObject GemPrefab;

        public int ScoreValue = 0;

        public float TweenIceBlockOutTime = 0.5f;

        public Ease TweenIceBlockOutEase = Ease.OutBack;

        // Properties

        private string ScoreText {
            get {
                return this.ScoreValue.ToString();
            }
        }

        // Private Methods
        
        public void DestroyIceBlock() {

            // Destroy physics
            Destroy(this.GetComponent<BoxCollider2D>());
            Destroy(this.GetComponent<Rigidbody2D>());

            // Scale the block to zero
            this.transform.DOScale(0, this.TweenIceBlockOutTime)
                .SetId(this.gameObject)
                .SetEase(this.TweenIceBlockOutEase);

            // Rotate the block
            this.transform.DORotate(new Vector3(0, 0, 720), this.TweenIceBlockOutTime, RotateMode.LocalAxisAdd)
                .SetId(this.gameObject)
                .SetEase(this.TweenIceBlockOutEase);

            // Destroy this block
            TimerSystem.CreateTimer("DestroyIceBlock_" + this.GUID, this.TweenIceBlockOutTime + 1)
                .Watch(() => {
                    DOTween.Kill(this.gameObject);
                    Destroy(this.gameObject);
                });
        }
    }
}