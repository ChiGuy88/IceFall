using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;
using DG.Tweening;

namespace IceFalls {

    public class script_Icicle : CRYSTAL_Script {

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
            if (collision.CompareTag("IceCollider") ||
              collision.CompareTag("IceBlock")  
            ) {
                this.SpawnIceBlock();
            }
        }

        private void HitPlayer(GameObject _PlayerObject) {
            CONSOLE.Log("Hit Player!");

            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<Rigidbody2D>());

            this.transform.DOMove(_PlayerObject.transform.position, Tween_PlayerHitTime)
                .SetEase(this.Tween_PlayerHitEase);
            this.transform.DOScale(Vector3.zero, Tween_PlayerHitTime)
                .SetEase(this.Tween_PlayerHitEase);

            if (this.PlayerHitPrefab != null) {
                GameObject clone = GO.Clone(this.PlayerHitPrefab, Vector3.zero);
                clone.transform.parent = null;
                clone.transform.position = _PlayerObject.transform.position;
                Destroy(clone, 2);
            }
            
            Destroy(this.gameObject, Tween_PlayerHitTime);
        }

        private void SpawnIceBlock() {
            CONSOLE.Log("Spawn Ice Block!");

            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<Rigidbody2D>());

            this.transform.DOScale(Vector3.zero, Tween_IceColliderHitTime)
                .SetEase(this.Tween_IceColliderHitEase);

            GameObject prefab = this.IceBlockPrefab();
            if (prefab != null) {
                GameObject iceBlockClone = GO.Clone(prefab, this.transform.position);
                iceBlockClone.transform.parent = GO.Find("IceBlocks").transform;
            }

            Destroy(this.gameObject, Tween_PlayerHitTime);
        }
    }
}