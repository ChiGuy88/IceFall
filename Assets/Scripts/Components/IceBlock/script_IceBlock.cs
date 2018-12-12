using UnityEngine;
using CRYSTAL;
using DG.Tweening;

namespace IceFalls {

    public class script_IceBlock : CRYSTAL_Script {

        // Public

        public readonly string GUID = System.Guid.NewGuid().ToString();

        public GameObject ScoreTextPrefab;

        public int ScoreValue = 0;

        public float TweenIceBlockOutTime = 0.5f;

        public Ease TweenIceBlockOutEase = Ease.OutBack;

        public float TweenScoreTextTime = 2f;

        public Ease TweenScoreTextEase = Ease.OutBack;

        private string ScoreText {
            get {
                return this.ScoreValue.ToString();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.CompareTag("Player")) {
                this.CollideWithPlayer(collision.gameObject);
            }
        }

        private void CollideWithPlayer(GameObject _PlayerObj) {

            // Add to total score
            GamePlayerPrefs.Instance.AddToScore(this.ScoreValue);

            // Display Score Points
            GameObject textClone = GO.Clone(this.ScoreTextPrefab, Vector3.zero);
            script_ScoreText scoreText = textClone.GetComponent<script_ScoreText>();
            textClone.transform.parent = _PlayerObj.transform;
            scoreText.DisplayText = this.ScoreText;

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

            // Play audio
            AudioSource audioSource = this.FindObjectInChildrenByName("CollectedAudio").GetComponent<AudioSource>();
            audioSource.Play();

            // Destroy this block
            TimerSystem.CreateTimer("DestroyIceBlock_" + this.GUID, this.TweenIceBlockOutTime + 1)
                .Watch(() => {
                    DOTween.Kill(this.gameObject);
                    Destroy(this.gameObject);
                });
        }
    }
}