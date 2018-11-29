using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace IceFalls {

    public class script_MainMenu : CRYSTAL_Script {

        public void CLICK_StartGame(GameObject target) {

            CONSOLE.Log("START GAME");
            SceneManager.LoadScene("Freeplay");
        }
    }
}