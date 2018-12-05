using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CRYSTAL;
using DG.Tweening;

namespace IceFalls {

    public class script_HUD_PlayerHealth : CRYSTAL_Script {

        public GameObject Life1;
        public GameObject Life2;
        public GameObject Life3;
        public GameObject Life4;
        public GameObject Life5;

        private Image p_Life1Image;
        private Image p_Life2Image;
        private Image p_Life3Image;
        private Image p_Life4Image;
        private Image p_Life5Image;

        private GameConfig p_GameConfig;

        public override void SetDefaultValues() {
            base.SetDefaultValues();

            this.p_Life1Image = Life1.GetComponent<Image>();
            this.p_Life2Image = Life2.GetComponent<Image>();
            this.p_Life3Image = Life3.GetComponent<Image>();
            this.p_Life4Image = Life4.GetComponent<Image>();
            this.p_Life5Image = Life5.GetComponent<Image>();

            this.p_GameConfig = GameConfig.Instance;

            this.UpdateLifeIcons();
        }

        public void PlayerHit() {
            CONSOLE.Log("HUD PLAYER HIT");

            this.UpdateLifeIcons();
        }

        private void UpdateLifeIcons() {

            this.p_Life1Image.DOColor((this.p_GameConfig.TotalLives >= 1) ? Color.white : new Color(0, 0, 0, 0), 1);
            this.p_Life2Image.DOColor((this.p_GameConfig.TotalLives >= 2) ? Color.white : new Color(0, 0, 0, 0), 1);
            this.p_Life3Image.DOColor((this.p_GameConfig.TotalLives >= 3) ? Color.white : new Color(0, 0, 0, 0), 1);
            this.p_Life4Image.DOColor((this.p_GameConfig.TotalLives >= 4) ? Color.white : new Color(0, 0, 0, 0), 1);
            this.p_Life5Image.DOColor((this.p_GameConfig.TotalLives >= 5) ? Color.white : new Color(0, 0, 0, 0), 1);
        }
    }
}