using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;

namespace IceFalls {

    public class script_IcicleSpawner : CRYSTAL_Script {

        public GameObject Prefab;

        public float TimeTillNextSpawn = 1;

        private float p_CurrentTimerCount = 0;

        public override void SetDefaultValues() {
            base.SetDefaultValues();

            if (this.Prefab != null) {
                this.SpawnIcicle();
            }
        }

        private void SpawnIcicle() {

            this.p_CurrentTimerCount = 0;

            GameObject clone = GO.Clone(this.Prefab, Vector3.zero);
            clone.transform.parent = this.transform;
            clone.transform.localPosition = Vector3.zero;
            clone = null;
        }

        public override void Step() {
            base.Step();

            if (this.TimeTillNextSpawn > 0) {

                this.p_CurrentTimerCount += Time.deltaTime;

                if (this.p_CurrentTimerCount >= this.TimeTillNextSpawn) {
                    this.SpawnIcicle();
                }
            }
        }
    }
}