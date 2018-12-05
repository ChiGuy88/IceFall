using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceFalls {

    public class GameConfig {

        private static GameConfig p_Instance = null;
        public static GameConfig Instance {
            get {
                if (p_Instance == null) {
                    p_Instance = new GameConfig();
                }
                return p_Instance;
            }
        }

        // Public Variables
        // -------------------

        public int TotalScore { get; private set; }

        public int TotalLives { get; private set; }

        // Private Variables
        // -------------------

        private int p_HighScore = 0;

        private int p_MaxLives = 5;

        // Properties
        // -------------------

        public int HighScore {
            get {
                return this.p_HighScore;
            }
        }

        public int MaxLives {
            get {
                return this.p_MaxLives;
            }
        }
        
        // Public Methods
        // ==============

        public GameConfig () {
            // Load saved player data
            this.p_HighScore = PlayerPrefs.GetInt("HighScore");
            this.p_MaxLives = PlayerPrefs.GetInt("MaxLives");

            if (p_MaxLives <= 0) {
                this.p_MaxLives = 5;
            }

            this.TotalLives = this.p_MaxLives;
        }

        public void SaveGameConfig() {
            PlayerPrefs.SetInt("HighScore", this.p_HighScore);
            PlayerPrefs.SetInt("MaxLives", this.p_MaxLives);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Value"></param>
        public void AddToScore(int _Value) {
            this.TotalScore += _Value;
        }

        public void LoseLife() {
            this.TotalLives--;

            if (this.TotalLives < 0) {
                // TODO: GAME OVER!
                script_Game.Instance.GameOver();
            }
        }

        public void GainLife() {
            this.TotalLives = Mathf.Min(this.TotalLives + 1, this.MaxLives);
        }
    }
}