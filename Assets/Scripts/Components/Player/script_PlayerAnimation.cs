using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;

namespace IceFalls {

    public class script_PlayerAnimation : CRYSTAL_Script {

        // Static

        public static script_PlayerAnimation Instance {
            get {
                return GO.Find("Player").GetComponent<script_PlayerAnimation>();
            }
        }

        // Properties

        private Animator p_Animator = null;
        public Animator Animator {
            get {
                if (this.p_Animator == null) {
                    this.p_Animator = this.GetComponentInChildren<Animator>();
                }
                return this.p_Animator;
            }
        }

        // Public Methods

        public void PlayIdleAnimation() {
            this.Animator.Play("Rogue_Idle");
        }

        public void PlayRunAnimation() {
            this.Animator.Play("Rogue_Run");
        }

        public void PlayDeathAnimation() {

        }
    }
}