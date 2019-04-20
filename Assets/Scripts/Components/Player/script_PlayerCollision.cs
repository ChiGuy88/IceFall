using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;
using DG.Tweening;

namespace IceFalls {

    public class script_PlayerCollision : CRYSTAL_Script {
        
        // Private Methods

        private void OnTriggerEnter2D(Collider2D collision) {

            if (collision.CompareTag("Icicle")) {
                this.PlayerHitIcicle();
                return;
            }

            if (collision.CompareTag("IceCollider")) {
                Destroy(collision.GetComponent<BoxCollider2D>());
                this.PlayerHitIceBlock(collision.transform.parent.gameObject);
                return;
            }
        }
        
        private void PlayerHitIcicle() {

            // Play hit audio
            script_AudioManager.Instance.PlaySoundEffect("PlayerHit");

            // shake the camera a bit
            Camera.main.DOShakePosition(0.25f, 1f);

            // Send message to other player scripts
            this.SendMessage("PlayerHit");
            GO.Find("HUD_PlayerHealth").SendMessage("PlayerHit");
        }

        public void PlayerHitIceBlock(GameObject _IceBlock) {

            script_IceBlock script_IceBlock = _IceBlock.GetComponent<script_IceBlock>();

            // Add to total score
            GameConfig.Instance.AddToScore((ulong) script_IceBlock.ScoreValue);

            // Tween gem to the score pile
            GameObject gemClone = GO.Clone(script_IceBlock.GemPrefab, _IceBlock.transform.position);
            Vector3 position = GO.Find("GEMS_TWEEN_POINT").transform.localPosition;

            gemClone.transform.DOScale(0f, 1f)
                .SetEase(Ease.InExpo);

            gemClone.transform.DOMove(position, 1f)
                .SetEase(Ease.OutExpo);

            Destroy(gemClone, 2f);

            script_IceBlock.DestroyIceBlock();

            // Play audio
            script_AudioManager.Instance.PlaySoundEffect("GemCollected");
        }
    }
}