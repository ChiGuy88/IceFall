using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;

namespace IceFalls {

    public class script_DoNotDestroy : CRYSTAL_Script {

        public override void SetDefaultValues() {
            base.SetDefaultValues();

            if (GameObject.FindGameObjectsWithTag("DoNotDestroy").Length > 1) {
                Destroy(this.gameObject);
                return;
            }

            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }
}