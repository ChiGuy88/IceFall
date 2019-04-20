using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;

namespace IceFalls {

    [Serializable]
    public class SerializedGameConfig {

        // Public

        //[SerializeField]
        //private bool ToggleFeature_Bombs;

        [SerializeField]
        private Dictionary__Int_ULong DictionaryHighScore;

        [SerializeField]
        private Dictionary__Int_Int DictionaryPlayerMaxHealth;

        [SerializeField]
        private Dictionary__Int_Int DictionaryPlayerRogueUnlockScores;

        [SerializeField]
        private Dictionary__Int_Bool DictionaryPlayerRogueLockedStatus;

        // Public Methods

        public SerializedGameConfig() {
            //
            //this.ToggleFeature_Bombs = true;
            this.DictionaryHighScore = new Dictionary__Int_ULong() {
                { 1, 0 },
                { 2, 0 },
                { 3, 0 },
                { 4, 0 },
                { 5, 0 },
                { 6, 0 }
            };
            //
            this.DictionaryPlayerMaxHealth = new Dictionary__Int_Int() {
                { 1, 5 },
                { 2, 6 },
                { 3, 7 },
                { 4, 8 },
                { 5, 9 },
                { 6, 10 }
            };
            //
            this.DictionaryPlayerRogueUnlockScores = new Dictionary__Int_Int() {
                { 1, 0     },
                { 2, 300   },
                { 3, 900   },
                { 4, 2700  },
                { 5, 8100  },
                { 6, 24300 }
            };
            //
            this.DictionaryPlayerRogueLockedStatus = new Dictionary__Int_Bool() {
                { 1, false },
                { 2, true },
                { 3, true },
                { 4, true },
                { 5, true },
                { 6, true }
            };
            //
        }

        public void SetHighScore(int _PlayerRogue, ulong _Score) {
            if (this.DictionaryHighScore.ContainsKey(_PlayerRogue) && this.DictionaryHighScore[_PlayerRogue] < _Score) {
                this.DictionaryHighScore[_PlayerRogue] = _Score;
            }
        }

        public ulong GetHighScore(int _PlayerRogue) {
            if (this.DictionaryHighScore.ContainsKey(_PlayerRogue)) {
                return this.DictionaryHighScore[_PlayerRogue];
            }
            return 0;
        }

        public int GetPlayerMaxHealth(int _PlayerRogue) {
            if (this.DictionaryPlayerMaxHealth.ContainsKey(_PlayerRogue)) {
                return this.DictionaryPlayerMaxHealth[_PlayerRogue];
            }
            return 0;
        }
        
        public int GetRogueUnlockScore(int _PlayerRogue) {
            if (this.DictionaryPlayerRogueUnlockScores.ContainsKey(_PlayerRogue)) {
                return this.DictionaryPlayerRogueUnlockScores[_PlayerRogue];
            }
            return 0;
        }

        public void SetPlayerRogueLockedStatus(int _PlayerRogue, bool _LockStatus) {
            if (this.DictionaryPlayerRogueLockedStatus.ContainsKey(_PlayerRogue)) {
                this.DictionaryPlayerRogueLockedStatus[_PlayerRogue] = _LockStatus;
            }
        }

        public bool GetPlayerRogueLockedStatus(int _PlayerRogue) {
            if (this.DictionaryPlayerRogueLockedStatus.ContainsKey(_PlayerRogue)) {
                return this.DictionaryPlayerRogueLockedStatus[_PlayerRogue];
            }
            return false;
        }
    }
}