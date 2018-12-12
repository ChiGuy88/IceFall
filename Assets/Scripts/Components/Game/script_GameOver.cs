using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;
using DG.Tweening;

namespace IceFalls {

    public class script_GameOver : CRYSTAL_Script {
        
        // Static 

        public static script_GameOver Instance {
            get {
                return GO.Find("Game").GetComponent<script_GameOver>();
            }
        }

        // Public

        public float TweenCameraToPlayerTime = 5f;
        public Ease TweenCameraToPlayerEase = Ease.OutBack;

        public float TweenTipPlayerOverTime = 1f;
        public Ease TweenTipPlayerOverEase = Ease.OutBounce;

        // Public Methods

        public void PlayGameOver() {

            script_Player2DMovement.Instance.DisableMovement();
            script_PlayerAnimation.Instance.Animator.SetBool("IsPlayerRunning", false);
            script_PlayerAnimation.Instance.PlayIdleAnimation();

            List<GameObject> icicleSpawners = new List<GameObject>(GameObject.FindGameObjectsWithTag("IcicleSpawner"));
            icicleSpawners.ForEach((GameObject o) => {
                o.GetComponent<script_IcicleSpawner>().DisableSpawner();
            });

            // Shake the Camera
            this.ShakeCamera(this.TweenCameraToPlayerTime, 0.75f);

            // Zoom in to the player
            this.CameraZoomOnPlayer(this.TweenCameraToPlayerTime);

            TimerSystem.CreateTimer("GameOver_CameraShakeTimer", this.TweenCameraToPlayerTime)
                .Watch(() => {
                    this.TipOverThePlayer();
                });
        }

        private void TipOverThePlayer() {

            // Rotate player
            float endRotation = GO.Find("Player").transform.Find("Rogue").transform.rotation.y == -180 ? -90 : 90;
            Destroy(GO.Find("Player").GetComponent<Rigidbody2D>());
            Destroy(GO.Find("Player").GetComponent<BoxCollider2D>());
            GO.Find("Player").transform.DORotate(new Vector3(0, 0, endRotation), this.TweenTipPlayerOverTime)
                .SetId(GO.Find("Player"))
                .SetEase(this.TweenTipPlayerOverEase);

            TimerSystem.CreateTimer("GameOver_CameraShakeTimer", this.TweenTipPlayerOverTime)
                .Watch(() => {
                    this.DisplayGameOverScreen();
                });
        }

        private void ShakeCamera(float _Time, float _Strength, bool _Repeat = false) {
            Camera.main.DOShakePosition(_Time, 0.5f);

            if (_Repeat) {
                TimerSystem.CreateTimer("ShakeCamera", _Time)
                    .Watch(() => {
                        this.ShakeCamera(_Time, _Strength, _Repeat);
                    });
            }
        }

        private void CameraZoomOnPlayer(float _Time) {
            Vector3 endPosition = GO.Find("Player").transform.position + Vector3.back * 5f;
            Camera.main.transform.DOMove(endPosition, _Time)
                .SetId(Camera.main)
                .SetEase(this.TweenCameraToPlayerEase);
        }

        private void DisplayGameOverScreen() {

            // Persist the high score
            GamePlayerPrefs.Instance.SaveGameConfig();

            // Reset to new game state
            GamePlayerPrefs.Instance.LoadGameConfig();

            // Display screen
            script_HUD_GameOverScreen.Instance.DisplayGameOverScreen();
        }
    }
}