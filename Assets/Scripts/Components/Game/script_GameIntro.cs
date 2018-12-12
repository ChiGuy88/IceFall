using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;
using DG.Tweening;

namespace IceFalls {

    public class script_GameIntro : CRYSTAL_Script {

        // Static 

        public static script_GameIntro Instance {
            get {
                return GO.Find("Game").GetComponent<script_GameIntro>();
            }
        }

        // Public Methods

        public void PlayGameIntro() {

            // Fun intro shake of camera
            script_Camera.Instance.ShakeCamera(2, 0.5f);
        }
    }
}