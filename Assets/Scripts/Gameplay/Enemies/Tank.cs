using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy {
    protected override void SoundAttack() {
        AkSoundEngine.PostEvent("jugger_attack", this.gameObject);
    }
}
