using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;
using UnityEngine.SceneManagement;
using TMPro;

namespace IceFalls {

    public class script_MainMenu : CRYSTAL_Script {

        public override void FixedStep() {
            base.FixedStep();

            int highScore = GamePlayerPrefs.Instance.HighScore;
            GO.Find("LABEL_HighScore").GetComponent<TextMeshProUGUI>().SetText("HIGH SCORE: " + highScore.ToString());
        }

        public void CLICK_StartGame(GameObject target) {
            SceneManager.LoadScene("2DIceFalls");
        }
    }
}