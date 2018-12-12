using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CRYSTAL;

namespace IceFalls {

    public class script_HUD_PlayerScore : CRYSTAL_Script {

        public override void FixedStep() {
            base.FixedStep();

            Text scoreText = this.FindObjectInChildrenByName("TEXT_Score").GetComponent<Text>();
            scoreText.text = GamePlayerPrefs.Instance.TotalScore.ToString();
        }
    }
}