using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;

namespace IceFalls {

    public class script_Player2DMovement : CRYSTAL_Script {

        public bool IsMovementEnabled = true;

        public float Force;

        public float PlayerRunVelocityXThreshold = 0.25f;

        private Animator p_Animator;

        public override void SetDefaultValues() {
            base.SetDefaultValues();

            this.p_Animator = this.GetComponentInChildren<Animator>();
        }

        public override void Step() {
            base.Step();

            if (!IsMovementEnabled) {
                return;
            }

            // Update Rigidbody from Input
            // ---------------------------
            GameObject rogueObj = this.FindObjectInChildrenByName("Rogue");
            Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D>();
            float horizontal = Input.GetAxis("Horizontal");
            float directionalVelocity = 0;
            bool isPlayerRunning = false;

            if (horizontal > 0.2f || horizontal < -0.2f) {
                
                if (horizontal > 0) {
                    directionalVelocity = Force;
                    rogueObj.transform.rotation = new Quaternion(0, 0, 0, 0);
                } 
                else if (horizontal < 0) {
                    directionalVelocity = -Force;
                    rogueObj.transform.rotation = new Quaternion(0, 180, 0, 0);
                }
                
                rigidbody.AddForce(Vector2.right * directionalVelocity);
                isPlayerRunning = Mathf.Abs(rigidbody.velocity.x) >= this.PlayerRunVelocityXThreshold;
            }

            // Update animator
            // ---------------
            float vel = Mathf.Abs(rigidbody.velocity.x);
            this.p_Animator.SetBool("IsPlayerRunning", isPlayerRunning);
            
            if (isPlayerRunning && !this.p_Animator.GetCurrentAnimatorStateInfo(0).IsName("Rogue_Run")) {
                this.p_Animator.Play("Rogue_Run");
            }
        }
    }
}