using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : Structure {
    [SerializeField] Fire fire;
    [SerializeField] float timeAttacking;
    [SerializeField] ParticleSystem ps;

    [SerializeField] bool fireOn = true;

    float timerFireOn = 0;
    float timerFireOff = 0;
    private void Update() {
        if (!defending)
            return;

        if (!fireOn) {
            timerFireOff += Time.deltaTime;
            if (timerFireOff >= attackPreparationTime) {
                timerFireOff = 0;
                fireOn = true;
                fire.gameObject.SetActive(true);
                fire.SetDamage(damage);
                ps.Play();
                AkSoundEngine.PostEvent("torret_fire", this.gameObject);
            }
            return;
        }

        timerFireOn += Time.deltaTime;
        if (timerFireOn >= timeAttacking) {
            fireOn = false;
            timerFireOn = 0;
            fire.gameObject.SetActive(false);
            ps.Stop();
        }
    }

}
