using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;

namespace IceFalls {

    public class script_DoNotDestroy : CRYSTAL_Script {

        public override void SetDefaultValues() {
            base.SetDefaultValues();

            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }
}