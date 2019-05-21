using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;

namespace IceFalls {

    public class script_AudioSource : CRYSTAL_Script {

        // Private

        private float p_MaxVolume;

        // Properties

        public AudioSource Source {
            get {
                return this.GetComponent<AudioSource>();
            }
        }

        // Public Methods

        public override void SetDefaultValues() {
            base.SetDefaultValues();

            this.p_MaxVolume = this.Source.volume;
        }

        public void UpdateVolume(float _Percentage) {
            this.Source.volume = this.p_MaxVolume * _Percentage;
        }
    }
}