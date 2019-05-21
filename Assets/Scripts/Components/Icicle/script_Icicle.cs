using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;
using DG.Tweening;

namespace IceFalls {

    public class script_Icicle : CRYSTAL_Script {

        // Public

        public readonly string GUID = System.Guid.NewGuid().ToString();

        // Collide (Player)
        public GameObject PlayerHitPrefab;
        public float Tween_PlayerHitTime = 1;
        public Ease Tween_PlayerHitEase = Ease.OutBack;

        // Collide (Ice Collider)
        public GameObject BlueBlockPrefab;
        public GameObject GreenBlockPrefab;
        public GameObject RedBlockPrefab;
        public float Tween_IceColliderHitTime = 1;
        public Ease Tween_IceColliderHitEase = Ease.OutBack;

        // Percent odds of colored blocks (0-1)
        public float GreenOdds = 0.2f;
        public float RedOdds = 0.05f;

        private GameObject IceBlockPrefab() {
            float randFloat = Random.value;

            if (randFloat <= RedOdds) {
                return RedBlockPrefab;
            }

            if (randFloat <= GreenOdds) {
                return GreenBlockPrefab;
            }

            return BlueBlockPrefab;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            
            // Icicle hit player
            if (collision.CompareTag("Player")) {
                this.HitPlayer(collision.gameObject);  
            }

            // Icicle hit ground/ice-block
            if (collision.CompareTag("IceBlock") ||
                collision.CompareTag("Floor")
            ) {
                this.SpawnIceBlock();
            }
        }

        private void HitPlayer(GameObject _PlayerObject) {

            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<Rigidbody2D>());

            this.transform.DOMove(_PlayerObject.transform.position, Tween_PlayerHitTime)
                .SetId(this.gameObject)
                .SetEase(this.Tween_PlayerHitEase);

            this.transform.DOScale(Vector3.zero, Tween_PlayerHitTime)
                .SetId(this.gameObject)
                .SetEase(this.Tween_PlayerHitEase);

            if (this.PlayerHitPrefab != null) {
                GameObject clone = GO.Clone(this.PlayerHitPrefab, Vector3.zero);
                clone.transform.position = _PlayerObject.transform.position;
                Destroy(clone, 2);
            }

            TimerSystem.CreateTimer("DestroyIcicle_" + this.GUID, this.Tween_PlayerHitTime)
                .Watch(() => {
                    DOTween.Kill(this.gameObject);
                    Destroy(this.gameObject, 1);
                });
        }

        private void SpawnIceBlock() {

            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<Rigidbody2D>());

            this.transform.localScale = Vector3.zero;

            GameObject prefab = this.IceBlockPrefab();
            if (prefab != null) {
                GameObject iceBlockClone = GO.Clone(prefab, this.transform.position);
                iceBlockClone.transform.parent = this.transform.parent;
                iceBlockClone.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                iceBlockClone.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0.25f);
            }

            TimerSystem.CreateTimer("DestroyIcicle_" + this.GUID, this.Tween_PlayerHitTime)
                .Watch(() => {
                    DOTween.Kill(this.gameObject);
                    Destroy(this.gameObject, 1);
                });
        }
    }
}