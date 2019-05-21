using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CRYSTAL;
using DG.Tweening;

namespace IceFalls {

    public class script_HUD_PlayerHealth : CRYSTAL_Script {

        public Ease TweenHealthOverdriveOut = Ease.OutBounce;

        public List<GameObject> LifeGameObjects = new List<GameObject>();

        private GameConfig p_GameConfig;

        public override void SetDefaultValues() {
            base.SetDefaultValues();

            this.p_GameConfig = GameConfig.Instance;

            this.UpdateLifeIcons();
        }

        public void PlayerHit() {
            CONSOLE.Log("HUD PLAYER HIT");

            this.UpdateLifeIcons();
        }

        private void UpdateLifeIcons() {
            
            int totalLives = this.p_GameConfig.TotalHealth;
            int maxHealth = this.p_GameConfig.MaxHealth;
            int healthDelta = totalLives - 5;
            Vector3 overdrivePos;
            Transform overdriveIconTransform;
            int i, n = this.LifeGameObjects.Count;
            for (i = 0; i < n; ++i) {

                if (this.LifeGameObjects[i] == null) { return; }

                overdrivePos = this.LifeGameObjects[i].transform.localPosition;
                overdriveIconTransform = this.LifeGameObjects[i].transform.Find("IMAGE_OverdriveIcon");

                this.LifeGameObjects[i].GetComponent<CanvasGroup>().alpha = maxHealth < (i + 1) ? 0 : 1;

                this.LifeGameObjects[i].transform.Find("IMAGE_OuterColor")
                    .GetComponent<Image>()
                    .DOFillAmount(totalLives >= (i + 1) ? 1 : 0, 1f);

                this.LifeGameObjects[i].transform.Find("IMAGE_LifeIcon")
                    .transform.DOScale(totalLives >= (i + 1) ? 1 : 0, 1f);

                overdriveIconTransform.DOScale(healthDelta >= (i + 1) ? 1 : 0, 1f)
                    .SetEase(this.TweenHealthOverdriveOut);

                overdriveIconTransform.DOLocalMoveY(healthDelta >= (i + 1) ? 0 : -75, 1f)
                    .SetEase(this.TweenHealthOverdriveOut);

                //if (healthDelta < (i + 1)) {

                //    CONSOLE.Log("Overdrive Lost!");
                    
                //    overdriveIconTransform.DOScale(2, 1f)
                //        .SetEase(this.TweenHealthOverdriveOut)
                //        .OnComplete(() => {
                //            overdriveIconTransform
                //                .DOScale(0, 1f)
                //                .SetEase(this.TweenHealthOverdriveOut);
                //        });

                //    overdriveIconTransform
                //        .DOLocalRotate(new Vector3(0, 0, 360 * 4), 2f)
                //        .SetEase(this.TweenHealthOverdriveOut);

                //    //this.LifeGameObjects[i].transform.Find("IMAGE_OverdriveIcon")
                //    //    .transform.DOShakePosition(1f)
                //    //    .SetEase(this.TweenHealthOverdriveOut);
                //}
                //else {

                //    this.LifeGameObjects[i].transform.Find("IMAGE_OverdriveIcon")
                //        .transform.DOScale(1, 1f);
                //}
            }
        }
    }
}