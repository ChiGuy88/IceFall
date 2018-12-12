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

        public override void SetDefaultValues() {
            base.SetDefaultValues();

            // Load in config, if the override is true then overrite the saved values
            //GameConfig loaded = FileSystem.LoadConfigFromFile<GameConfig>("Configs/GameConfig.json");
            //CONSOLE.Log(loaded);
        }

        public override void StartGame() {
            base.StartGame();

            // Play the game intro
            script_GameIntro.Instance.PlayGameIntro();
        }

        public override void EndGame() {
            base.EndGame();

            // Play game over animation
            script_GameOver.Instance.PlayGameOver();
        }
    }
}
