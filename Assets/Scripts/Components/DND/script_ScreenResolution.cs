using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;

namespace IceFalls {

    public class script_ScreenResolution : CRYSTAL_Script {

        private int p_ScreenResolution = 0;

        /*
              { x: 256, y: 144 },
              { x: 426, y: 240 },
              { x: 640, y: 360 },
              { x: 800, y: 450 },
              { x: 960, y: 540 },
              { x: 1024, y: 576 },
              { x: 1280, y: 720 },
              { x: 1366, y: 768 },
              { x: 1600, y: 900 },
              { x: 1920, y: 1080 },
              { x: 2560, y: 1440 }
         */

        private List<Vector2> p_ScreenResolutions = new List<Vector2>() {
            new Vector2(256, 144),
            new Vector2(426, 240),
            new Vector2(640, 360),
            new Vector2(800, 450),
            new Vector2(960, 540),
            new Vector2(1024, 576),
            new Vector2(1280, 720),
            new Vector2(1366, 768),
            new Vector2(1600, 900),
            new Vector2(1920, 1080),
            new Vector2(2560, 1440)
        };

        public override void SetDefaultValues() {
            base.SetDefaultValues();

            this.SetResolution();
        }

        public void SetResolution() {

            Vector2 screenRes = this.p_ScreenResolutions[this.p_ScreenResolution];

            while (this.p_ScreenResolution > 0 && (Screen.width < screenRes.x || Screen.height < screenRes.y)) {
                this.p_ScreenResolution--;
                screenRes = this.p_ScreenResolutions[this.p_ScreenResolution];

                Camera.main.transform.position += Vector3.back * (3 / this.p_ScreenResolution);
            }

            Screen.SetResolution((int)screenRes.x, (int)screenRes.y, FullScreenMode.Windowed);
        }
    }
}