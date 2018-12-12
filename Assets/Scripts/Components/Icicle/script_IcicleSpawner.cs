using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;

namespace IceFalls {

    public class script_IcicleSpawner : CRYSTAL_Script {

        // Public

        public GameObject Prefab;

        public int TotalIceBlocksToFormWall = 4;

        public float TimeTillNextSwitchUp = 0;
        public float SwitchUpTime_MinMin = 0;
        public float SwitchUpTime_MinMax = 0;
        
        public float SwitchUpTime_MaxMin = 0;
        public float SwitchUpTime_MaxMax = 0;

        public float MinTimeTillNextSpawn = 0;
        public float MaxTimeTillNextSpawn = 0;

        // Private 

        private float p_TimeTillNextSpawn = 0;

        private float p_CurrentTimerTillNextSpawn = 0;

        private float p_CurrentTimerTillNextSwitchUp = 0;

        // Properties

        public bool IsSpawnerEnabled { get; private set; }

        public float TimeTillNextSpawn {
            get {
                return Random.Range(this.MinTimeTillNextSpawn, this.MaxTimeTillNextSpawn);
            }
        }

        // Public Methods

        public override void SetDefaultValues() {
            base.SetDefaultValues();

            this.IsSpawnerEnabled = true;
            this.p_TimeTillNextSpawn = this.TimeTillNextSpawn;
        }

        public override void Step() {
            base.Step();

            if (!this.IsSpawnerEnabled) {
                return;
            }

            // Update SwitchUp timer
            if (this.TimeTillNextSwitchUp > 0) {
                this.p_CurrentTimerTillNextSwitchUp += Time.deltaTime;

                if (this.p_CurrentTimerTillNextSwitchUp >= this.TimeTillNextSwitchUp) {
                    this.p_CurrentTimerTillNextSwitchUp = 0;
                    this.SwitchUp();
                }
            }

            // Update icicle spawn timer
            if (this.TimeTillNextSpawn > 0 && this.transform.childCount < this.TotalIceBlocksToFormWall) {
                this.p_CurrentTimerTillNextSpawn += Time.deltaTime;

                if (this.p_CurrentTimerTillNextSpawn >= this.p_TimeTillNextSpawn) {
                    this.p_CurrentTimerTillNextSpawn = 0;
                    this.SpawnIcicle();
                }
            }
        }

        public void DisableSpawner() {
            this.IsSpawnerEnabled = false;
        }
        
        public void EnableSpawner() {
            this.IsSpawnerEnabled = true;
        }

        // Private Methods

        private void SwitchUp() {
            float minTime, maxTime;

            do {
                minTime = Random.Range(this.SwitchUpTime_MinMin, this.SwitchUpTime_MinMax);
                maxTime = Random.Range(this.SwitchUpTime_MaxMin, this.SwitchUpTime_MaxMax);
            } while (minTime >= maxTime);

            this.MinTimeTillNextSpawn = minTime;
            this.MaxTimeTillNextSpawn = maxTime;
        }

        private void SpawnIcicle() {

            this.p_TimeTillNextSpawn = this.TimeTillNextSpawn;

            GameObject clone = GO.Clone(this.Prefab, Vector3.zero);
            clone.transform.parent = this.transform;
            clone.transform.localPosition = Vector3.zero;
            clone = null;
        }
    }
}