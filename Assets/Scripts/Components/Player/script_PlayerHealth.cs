using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;
using DG.Tweening;

namespace IceFalls {

    public class script_PlayerHealth : CRYSTAL_Script {

        // Public
        // ------

        public float PlayerDamageTweenTime = 1;

        public Ease PlayerDamageTweenInEase = Ease.OutBack;

        public Ease PlayerDamageTweenOutEase = Ease.InBack;

        public Color PlayerDamageTweenStartColor = Color.white;

        public Color PlayerDamageTweenEndColor = Color.red;

        // Private
        // -------

        private Animator p_Animator;

        public override void SetDefaultValues() {
            base.SetDefaultValues();
            this.p_Animator = this.GetComponentInChildren<Animator>();
        }

        public void PlayerHit() {

            // Lose Life
            GameConfig.Instance.LoseLife();

            // Play hit animation
            this.p_Animator.Play("Rogue_Hit");

            // Tween player color to indicate damage
            List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>(this.GetComponentsInChildren<SpriteRenderer>());
            this.TweenPlayerDamageIn(spriteRenderers);
        }

        private void TweenPlayerDamageIn(List<SpriteRenderer> spriteRenderers) {
            
            spriteRenderers.ForEach((SpriteRenderer renderer) => {
                DOTween.Kill(renderer);
                renderer.DOColor(this.PlayerDamageTweenEndColor, this.PlayerDamageTweenTime)
                .SetId(renderer)
                .SetEase(this.PlayerDamageTweenInEase);
            });

            TimerSystem.RemoveTimer("PlayerDamageTweenToNormal");
            TimerSystem.CreateTimer("PlayerDamageTweenToNormal", this.PlayerDamageTweenTime)
                .Watch(() => {
                    this.TweenPlayerDamageOut(spriteRenderers);
                });
        }

        private void TweenPlayerDamageOut(List<SpriteRenderer> spriteRenderers) {

            spriteRenderers.ForEach((SpriteRenderer renderer) => {
                DOTween.Kill(renderer);
                renderer.DOColor(this.PlayerDamageTweenStartColor, this.PlayerDamageTweenTime)
                .SetId(renderer)
                .SetEase(this.PlayerDamageTweenInEase);
            });
        }
    }
}