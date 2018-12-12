using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CRYSTAL;

namespace IceFalls {

    public class script_Bootstrap : CRYSTAL_Game {
        
        public override void SetDefaultValues() {
            base.SetDefaultValues();

            SceneManager.LoadScene("MainMenu");
        }
    }
}