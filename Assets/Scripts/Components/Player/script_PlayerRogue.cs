using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;

namespace IceFalls {

    public class script_PlayerRogue : CRYSTAL_Script {

        public List<GameObject> PlayerRoguePrefabs = new List<GameObject>();

        public override void SetDefaultValues() {
            base.SetDefaultValues();

            // Create the player rogue selected
            this.GeneratePlayerRogue(GameConfig.Instance.PlayerRogue);
        }

        public void GeneratePlayerRogue(int _Index) {

            GameObject prefab = this.PlayerRoguePrefabs[_Index - 1]; // Index for Rogue's starts at 1, instead of 0, just subtract 1
            GameObject rogueObject = this.transform.Find("Rogue").gameObject;

            if (rogueObject != null) {
                Destroy(rogueObject);
                rogueObject = null;
            }

            rogueObject = GO.Clone(prefab, Vector3.zero);
            rogueObject.transform.parent = this.transform;
            rogueObject.transform.localPosition = Vector3.zero;
            rogueObject.name = "Rogue";
        }
    }
}