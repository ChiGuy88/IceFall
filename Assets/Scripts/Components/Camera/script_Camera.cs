using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;
using DG.Tweening;

namespace IceFalls {

    public class script_Camera : CRYSTAL_Script {

        // Static

        public static script_Camera Instance {
            get {
                return Camera.main.GetComponent<script_Camera>();
            }
        }

        // Public Methods

        public void ShakeCamera(float _Time, float _Strength, bool _Repeat = false) {
            Camera.main.DOShakePosition(_Time, 0.5f);

            if (_Repeat) {
                TimerSystem.CreateTimer("ShakeCamera", _Time)
                    .Watch(() => {
                        this.ShakeCamera(_Time, _Strength, _Repeat);
                    });
            }
        }
    }
}