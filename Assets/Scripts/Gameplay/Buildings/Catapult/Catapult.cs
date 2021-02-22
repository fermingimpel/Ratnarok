using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : Structure {
    [SerializeField] Bullet shoot;
    [SerializeField] Vector3 offset;
    float timerToAttack = 0f;

    private void Update() {
        if (!defending)
            return;

        timerToAttack += Time.deltaTime;
        if (timerToAttack >= attackPreparationTime) {
            timerToAttack = 0;
            Attack();
        }
    }
    protected override void Attack() {
        animator.Play("Shoot");
        AkSoundEngine.PostEvent("torret_catapult", this.gameObject);
        Bullet s = Instantiate(shoot, transform.position + offset, shoot.transform.rotation);
        s.SetDirection(transform.forward + offset);
        s.SetDamage(damage);
    }

}
