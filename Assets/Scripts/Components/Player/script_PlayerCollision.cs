using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;
using DG.Tweening;

namespace IceFalls {

    public class script_PlayerCollision : CRYSTAL_Script {

        private void OnTriggerEnter2D(Collider2D collision) {
            
            if (collision.gameObject.CompareTag("Icicle")) {

                // Play hit audio
                AudioSource audioSource = this.FindObjectInChildrenByName("HitAudio").GetComponent<AudioSource>();
                audioSource.Play();

                // shake the camera a bit
                Camera.main.DOShakePosition(0.25f, 1f);

                // Send message to other player scripts
                this.SendMessage("PlayerHit");
                GO.Find("HUD_PlayerLives").SendMessage("PlayerHit");
            }
        }
    }
}