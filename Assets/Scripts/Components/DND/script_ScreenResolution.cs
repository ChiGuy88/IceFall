using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;

namespace IceFalls {

    public class script_ScreenResolution : CRYSTAL_Script {

        public int ScreenWidth = 1920;

        public int ScreenHeight = 1080;

        public override void SetDefaultValues() {
            base.SetDefaultValues();

            this.SetResolution();
        }

        public void SetResolution() {
            Screen.SetResolution(this.ScreenWidth, this.ScreenHeight, false);
        }
    }
}