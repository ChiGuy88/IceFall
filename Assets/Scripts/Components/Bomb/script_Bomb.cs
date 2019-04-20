using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;
using DG.Tweening;

namespace IceFalls {

    public class script_Bomb : CRYSTAL_Script {

        public string GUID { get; private set; }

        public float BombExplodeResetTimer = 3f;

        public GameObject ExplosionPrefab;

        public float ExplosionRadius = 2f;

        public float PushbackForceMultiplier = 200;
        
        private bool p_HasExploded;

        private string BombTimer {
            get {
                return "BombTimer_" + this.GUID;
            }
        }

        public override void SetDefaultValues() {
            base.SetDefaultValues();

            this.GUID = System.Guid.NewGuid().ToString();

            //
        }

        private void OnCollisionEnter2D(Collision2D collision) {
           
            if (collision.collider.CompareTag("Player") ||
                collision.collider.CompareTag("Floor")) {
                this.Explode();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            
            if (collision.CompareTag("IceCollider")) {
                CONSOLE.Log("Collision [ Bomb hit IceBlock ]");
                this.DestroyIceBlock(collision.gameObject);
            }
        }

        private void DestroyIceBlock(GameObject _IceBlock) {

            Transform parent = _IceBlock.transform.parent;

            Destroy(parent.GetComponent<Rigidbody2D>());
            Destroy(parent.GetComponent<BoxCollider2D>());
            Destroy(_IceBlock.GetComponent<BoxCollider2D>());

            parent.DOScale(0, 0.35f)
                .SetEase(Ease.OutBack)
                .OnComplete(() => {
                    Destroy(parent.gameObject);
                });
        }

        private void Explode() {

            // Collision with player will take damage
            float distanceToPlayer = (script_Player2DMovement.Instance.transform.position - this.transform.position).magnitude;
            float pushbackForce;
            CONSOLE.Log("Distance to player:", distanceToPlayer);
            if (distanceToPlayer <= this.ExplosionRadius) {
                pushbackForce = (this.ExplosionRadius - distanceToPlayer) * this.PushbackForceMultiplier;
                if (script_Player2DMovement.Instance.transform.position.x < this.transform.position.x) {
                    script_Player2DMovement.Instance.GetComponent<Rigidbody2D>().AddForce(Vector2.left * pushbackForce);
                }
                else {
                    script_Player2DMovement.Instance.GetComponent<Rigidbody2D>().AddForce(Vector2.right * pushbackForce);
                }

                script_PlayerHealth.Instance.PlayerHit();
                CONSOLE.Warn("|BOMB HIT PLAYER|");
            }

            // Spawn explosion
            GameObject explosionClone = GO.Clone(this.ExplosionPrefab, Vector3.zero);
            explosionClone.transform.parent = this.transform;
            explosionClone.transform.localPosition = Vector3.zero;

            // Destroy Bomb & explosion obj
            Destroy(this.GetComponent<SpriteRenderer>());
            Destroy(this.GetComponent<Rigidbody2D>());
            Destroy(this.GetComponent<BoxCollider2D>());
            Destroy(explosionClone, 1f);

            // Play explosion audio
            script_AudioManager.Instance.PlaySoundEffect("BombExplosion");

            // Reset the Spawner
            TimerSystem.CreateTimer(this.BombTimer, this.BombExplodeResetTimer)
                .Watch(() => {
                    this.transform.parent.GetComponent<script_IcicleSpawner>().EnableSpawner();
                    this.transform.parent.GetComponent<script_IcicleSpawner>().ResetIceWall();
                    Destroy(this.gameObject);
                });
        }
    }
}