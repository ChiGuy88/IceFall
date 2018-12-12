using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CRYSTAL;
using DG.Tweening;

namespace IceFalls {

    public class script_HUD_GameOverScreen : CRYSTAL_Script {

        // Static

        public static script_HUD_GameOverScreen Instance {
            get {
                return GO.Find("HUD_GameOverScreen").GetComponent<script_HUD_GameOverScreen>();
            }
        }

        // Public

        public float TweenGameOverScreenTime = 1f;

        public Ease TweenGameOverScreenEase = Ease.OutBack;

        public int MovedAgainButtonMoveGimmicCount = 2;

        // Private

        private bool p_IsGameOverScreenActive = false;

        private int p_MovedGoAgainButtonCount = 0;

        // Public Methods

        public override void Step() {
            base.Step();

            if (this.p_IsGameOverScreenActive && this.p_MovedGoAgainButtonCount < this.MovedAgainButtonMoveGimmicCount) {
                
            }
        }

        public void CLICK_GoAgain() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void CLICK_MainMenu() {
            SceneManager.LoadScene("MainMenu");
        }

        public void DisplayGameOverScreen() {

            this.p_MovedGoAgainButtonCount = 0;

            CanvasGroup group = this.GetComponent<CanvasGroup>();
            DOTween.To(
                () => group.alpha,
                (float value) => {
                    group.alpha = value;
                },
                1,
                this.TweenGameOverScreenTime
            )
            .SetId(this.gameObject)
            .SetEase(this.TweenGameOverScreenEase)
            .OnComplete(this.EnableGameOverScreenButtons);
        }

        private void EnableGameOverScreenButtons() {
            GO.Find("BUTTON_GoAgain").GetComponent<Button>().interactable = true;
            GO.Find("BUTTON_MainMenu").GetComponent<Button>().interactable = true;
        }
    }
}