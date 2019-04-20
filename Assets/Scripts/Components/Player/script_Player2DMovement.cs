using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;

namespace IceFalls {

    public class script_Player2DMovement : CRYSTAL_Script {

        // Static

        public static script_Player2DMovement Instance {
            get {
                return GO.Find("Player").GetComponent<script_Player2DMovement>();
            }
        }

        // Public

        public bool IsMovementEnabled = true;

        public float Force;

        public float PlayerRunVelocityXThreshold = 0.25f;

        // Public Methods

        public override void Step() {
            base.Step();

            // BAIL!
            if (!IsMovementEnabled) { return; }

            // Get Input based on platform
            float horizontal = 0;
            switch (Application.platform) {
                case RuntimePlatform.IPhonePlayer:
                case RuntimePlatform.Android:
                    if (Input.touchCount > 0) {
                        Touch t = Input.GetTouch(0);
                        horizontal = t.position.x >= 0 ? 1 : -1;
                    }
                    break;
                case RuntimePlatform.WebGLPlayer:
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                default:
                    horizontal = Input.GetAxis("Horizontal");
                    break;
            }

            // Update Rigidbody from Input
            GameObject rogueObj = this.FindObjectInChildrenByName("Rogue");
            Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D>();
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
            script_PlayerAnimation.Instance.Animator.SetBool("IsPlayerRunning", isPlayerRunning);
            
            if (isPlayerRunning && !script_PlayerAnimation.Instance.Animator.GetCurrentAnimatorStateInfo(0).IsName("Rogue_Run")) {
                script_PlayerAnimation.Instance.PlayRunAnimation();
            }
        }

        public void DisableMovement() {
            this.IsMovementEnabled = false;
        }

        public void EnableMovement() {
            this.IsMovementEnabled = true;
        }
    }
}