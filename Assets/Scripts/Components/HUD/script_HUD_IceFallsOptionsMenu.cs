using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CRYSTAL;
using DG.Tweening;

namespace IceFalls {

    public class script_HUD_IceFallsOptionsMenu : CRYSTAL_Script {

        // Static 

        public static script_HUD_IceFallsOptionsMenu Instance {
            get {
                return GO.Find("HUD_OptionsMenu").GetComponent<script_HUD_IceFallsOptionsMenu>();
            }
        }

        // Public

        public Ease TweenMenuInEase = Ease.OutBack;
        public float TweenMenuInTime = 1f;

        public Ease TweenMenuOutEase = Ease.InBack;
        public float TweenMenuOutTime = 0.5f;

        // Public Methods

        public void ShowMenu() {

            this.UpdateBackgroundMusic(script_AudioManager.Instance.BackgroundMusicVolume);

            DOTween.Kill(this.gameObject);

            RectTransform rectTransform = this.GetComponent<RectTransform>();
            rectTransform.DOScaleY(1, this.TweenMenuInTime)
                .SetId(this.gameObject)
                .SetEase(this.TweenMenuInEase);
        }

        public void HideMenu() {

            DOTween.Kill(this.gameObject);

            RectTransform rectTransform = this.GetComponent<RectTransform>();
            rectTransform.DOScaleY(0, this.TweenMenuOutTime)
                .SetId(this.gameObject)
                .SetEase(this.TweenMenuOutEase);
        }

        public void CLICK_RestoreToDefault() {
            this.UpdateBackgroundMusic(1);
        }

        public void CLICK_Return() {
            this.HideMenu();
        }

        public void CLICK_MainMenu() {

            // Save the game config
            GameConfig.Instance.SaveGameConfig();

            // Kill all tweens before loading scene
            DOTween.KillAll();

            // Load the MainMenu scene
            SceneManager.LoadScene("MainMenu");
        }

        public void SLIDER_BackgroundMusicVolume_DataChanged(GameObject _SliderObject) {

            float percentage = _SliderObject.GetComponent<Slider>().value;

            // Update UI Slider
            this.UpdateBackgroundMusic(percentage);

            // Update audio manager
            script_AudioManager.Instance.UpdateBackgroundMusicVolume("IceFallsBackgroundMusic");
        }

        // Private Methods

        private void UpdateBackgroundMusic(float _Value) {

            // Update Options UI slider value
            GameObject sliderBGM = this.transform.Find("SLIDER_BackgroundMusicVolume").gameObject;
            sliderBGM.GetComponent<Slider>().value = _Value;

            script_AudioManager.Instance.BackgroundMusicVolume = _Value;
        }
    }
}