using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;
using DG.Tweening;

namespace IceFalls {

    public class script_Game : CRYSTAL_Game {
        
        // STATIC
        // -------------------

        public static script_Game Instance {
            get {
                return GO.Find("Game").GetComponent<script_Game>();
            }
        }

        // Public Variables
        // --------------------

        public override void SetDefaultValues() {
            base.SetDefaultValues();

            // Fun intro shake of camera
            ShakeCamera(2, 0.5f);


            // Load in config, if the override is true then overrite the saved values
            // TODO: ==>
        }

        public void GameOver() {

            List<GameObject> icicleSpawners = new List<GameObject>(GameObject.FindGameObjectsWithTag("IcicleSpawner"));
            icicleSpawners.ForEach((GameObject o) => {
                o.SetActive(false);
            });

            ShakeCamera(10, 0.75f, true);
        }

        private void ShakeCamera(float _Time, float _Strength, bool _Repeat = false) {
            Camera.main.DOShakePosition(2, 0.5f);

            if (_Repeat) {
                TimerSystem.CreateTimer("ShakeCamera", 1)
                    .Watch(() => {
                        ShakeCamera(_Time, _Strength, _Repeat);
                    });
            }
        }
    }
}
