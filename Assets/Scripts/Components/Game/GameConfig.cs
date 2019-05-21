using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;
using DG.Tweening;

namespace IceFalls {
    
    public class GameConfig {

        private static string PERSIST_GAMECONFIG = "PERSIST_GAMECONFIG";

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

        public SerializedGameConfig Config;

        public ulong TotalScore { get; private set; }

        public int TotalHealth { get; private set; }

        // Private Variables
        // -------------------

        private int p_PlayerRogue = 6; // 1 - 6

        // Properties
        // -------------------
        
        public ulong HighScore {
            get {
                return this.Config.GetHighScore(this.PlayerRogue);
            }
        }

        public int MaxHealth {
            get {
                return this.Config.GetPlayerMaxHealth(this.PlayerRogue);
            }
        }
        
        public int PlayerRogue {
            get {
                return this.p_PlayerRogue;
            }
        }

        // Public Methods
        // ==============

        public GameConfig () {

            // Load saved player data
            this.LoadGameConfig();

            // Reset GamePlay Variables
            this.ResetGame();
        }

        public void ResetGame() {
            this.TotalHealth = this.MaxHealth;
            this.TotalScore = 0;
        }
        
        public void AddToScore(ulong _Value) {

            // Add to total score
            this.TotalScore += _Value;

            // Update config
            this.Config.SetHighScore(this.PlayerRogue, this.TotalScore);

            // Unlock next hero check
            if (this.PlayerRogue < 6 &&
                this.Config.GetPlayerRogueLockedStatus(this.PlayerRogue + 1) &&
                this.TotalScore >= (ulong) this.Config.GetRogueUnlockScore(this.PlayerRogue)
            ) {
                this.Config.SetPlayerRogueLockedStatus(this.PlayerRogue + 1, false);
            }

            // Shake score icon when player scores points
            GameObject scoreIcon = GO.Find("HUD_Gems");
            DOTween.Kill(scoreIcon, true);
            scoreIcon.transform.DOShakePosition(0.5f, _Value)
                .SetId(scoreIcon);
        }

        public void LoseHealth() {
            this.TotalHealth--;

            // GAME OVER!
            if (this.TotalHealth < 0) {

                // Persiste game and reset
                this.SaveGameConfig();

                // Play the game over animation
                script_GameOver.Instance.PlayGameOver();
            }
        }

        public void GainHealth() {
            this.TotalHealth = Mathf.Min(this.TotalHealth + 1, this.MaxHealth);
        }

        public void SetPlayerRogue(int _RogueNumber) {
            if (_RogueNumber < 1 || _RogueNumber > 6) {
                CONSOLE.Warn("Invalid Player Rogue Number: ", _RogueNumber);
                return;
            }

            this.p_PlayerRogue = _RogueNumber;
        }

        public bool IsPlayerRogueLocked(int _PlayerRogue) {
            return this.Config.GetPlayerRogueLockedStatus(_PlayerRogue);
        }

        public void LoadGameConfig() {

            try {
                this.Config = FileSystem.LoadConfigFromFile<SerializedGameConfig>(PERSIST_GAMECONFIG);
            }
            catch {
                this.Config = null;
            }

            if (this.Config == null) {
                this.Config = new SerializedGameConfig();
            }
        }

        public void SaveGameConfig() {

            if (this.Config == null) {
                CONSOLE.Warn("Game Config Is Null!!");
                return;
            }

            FileSystem.SaveFileFromConfig(this.Config, PERSIST_GAMECONFIG);
        }
    }
}