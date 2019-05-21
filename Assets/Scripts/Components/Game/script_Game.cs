using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;
using DG.Tweening;

namespace IceFalls {

    public class script_Game : CRYSTAL_Game {
        
        // Static

        public static script_Game Instance {
            get {
                return GO.Find("Game").GetComponent<script_Game>();
            }
        }

        // Public Methods

        public override void StartGame() {
            base.StartGame();

            script_AudioManager.Instance.PlayBackgroundMusic("IceFallsBackgroundMusic");

            // Play the game intro
            script_GameIntro.Instance.PlayGameIntro();
        }
    }
}
