using UnityEngine;
using CRYSTAL;
using TMPro;
using DG.Tweening;

namespace IceFalls {

    public class script_HUD_PlayerScore : CRYSTAL_Script {

        // Static

        public static script_HUD_PlayerScore Instance {
            get {
                return GO.Find("HUD_PlayerScore").GetComponent<script_HUD_PlayerScore>();
            }
        }

        // Public Methods

        public override void FixedStep() {
            base.FixedStep();

            TextMeshProUGUI scoreText = this.FindObjectInChildrenByName("TEXT_Score").GetComponent<TextMeshProUGUI>();
            scoreText.text = string.Format("{0:n0}", GameConfig.Instance.TotalScore);
        }
    }
}