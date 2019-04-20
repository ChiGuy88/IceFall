using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CRYSTAL;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

namespace IceFalls {

    public class script_HUD_MainMenu : CRYSTAL_Script {

        // Public

        public float DelayTimeTillButtonsAreEnabled = 2f;

        public float OptionsRotationSpeed = -2f;

        public float TweenButtonInTime = 2f;
        public Ease TweenButtonInEase = Ease.OutElastic;

        public float TweenButtonOutTime = 1f;
        public Ease TweenButtonOutEase = Ease.InBack;

        public Sprite UnlockedButtonSprite;

        public Sprite LockedButtonSprite;

        public List<GameObject> PlayerRogueButtonObjects = new List<GameObject>();

        // Private

        private bool p_IsOptionsButtonHover = false;

        // Public Methods

        public override void SetDefaultValues() {
            base.SetDefaultValues();

            script_AudioManager.Instance.PlayBackgroundMusic("MainMenuBackgroundMusic");

            // TODO: Unlock heroes that the player has unlocked (they are locked by default)
            int playerRogue = 1;
            bool isPlayerRogueLocked;
            Image buttonImage;
            Button buttonComp;
            PlayerRogueButtonObjects.ForEach((GameObject button) => {
                isPlayerRogueLocked = GameConfig.Instance.IsPlayerRogueLocked(playerRogue);
                buttonImage = button.GetComponent<Image>();
                buttonComp = button.GetComponent<Button>();
                
                if (buttonImage != null) {
                    buttonImage.sprite = isPlayerRogueLocked ? this.LockedButtonSprite : this.UnlockedButtonSprite;
                    buttonComp.interactable = !isPlayerRogueLocked;
                }

                playerRogue++;
            });
        }

        public override void FixedStep() {
            base.FixedStep();

            ulong highScore = GameConfig.Instance.HighScore;
            GO.Find("TEXT_HighScore").GetComponent<TextMeshProUGUI>().SetText("HIGH SCORE: " + highScore.ToString());
        }

        public void CLICK_StartGame() {

            DOTween.KillAll();

            SceneManager.LoadScene("2DIceFalls");
        }

        public void CLICK_PlayerRogue(int _RogueNumber) {

            // Update player rogue number
            GameConfig.Instance.SetPlayerRogue(_RogueNumber);
        }

        public void CLICK_OptionsMenu() {

            // Show the options button
            script_HUD_OptionsMenu.Instance.ShowMenu();
        }

        public void POINTER_ENTER_OptionsButton() {
            this.p_IsOptionsButtonHover = true;
        }

        public void POINTER_EXIT_OptionsButton() {
            this.p_IsOptionsButtonHover = false;
        }

        public void POINTER_ENTER_PlayerRogue(GameObject _Button) {

            Button button = _Button.GetComponent<Button>();

            // BAIL!
            if (!button.interactable) { return; }

            // Setup Tween
            DOTween.Kill(_Button);
            DOTween.To(
                ()=> _Button.transform.localScale,
                (Vector3 value) => {
                    if (_Button == null) { return; }
                    _Button.transform.localScale = value;
                },
                new Vector3(1.5f, 1.5f, 1.5f),
                this.TweenButtonInTime
            )
                .SetId(_Button)
                .SetEase(this.TweenButtonInEase);
        }

        public void POINTER_EXIT_PlayerRogue(GameObject _Button) {

            // BAIL!
            if (_Button == null) { return; }

            Button button = _Button.GetComponent<Button>();
            Vector3 startScale = button.transform.localScale;

            // BAIL!
            if (!button.interactable) { return; }

            // Setup Tween
            DOTween.Kill(_Button);
            DOTween.To(
                () => startScale,
                (Vector3 value) => {
                    if (_Button == null) { return; }
                    _Button.transform.localScale = value;
                },
                Vector3.one,
                this.TweenButtonOutTime
            )
                .SetId(_Button)
                .SetEase(this.TweenButtonOutEase);
        }
    }
}