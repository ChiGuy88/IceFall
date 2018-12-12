using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;

namespace IceFalls {

    public class script_MovingFog : CRYSTAL_Script {

        public int OffscreenPaddingPixels = 0;

        public float MovementSpeed = 1;

        public Vector3 Direction = Vector3.right;


        public override void Step() {
            base.Step();

            if (Mathf.Abs(this.transform.position.x) > OffscreenPaddingPixels) {
                this.transform.position = new Vector3(Direction.x * -OffscreenPaddingPixels, this.transform.position.y, this.transform.position.z);
            }
            else {
                this.transform.position += Direction * (MovementSpeed * Time.deltaTime);
            }
        }
    }
}