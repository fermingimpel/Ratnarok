using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : Structure {
    [SerializeField] Bullet shoot;
    [SerializeField] Vector3 upset;
    float timerToAttack = 0;
    private void Update() {
        if (!defending) return;

        timerToAttack += Time.deltaTime;
        if (timerToAttack >= attackPreparationTime) {
            timerToAttack = 0;
            Attack();
        }
    }
    protected override void Attack() {
        AkSoundEngine.PostEvent("torret_slingshot", this.gameObject);
        ratAnimator.Play("CrossbowAttack");
        animator.Play("Shoot");
        Bullet s = Instantiate(shoot, transform.position + upset, Quaternion.identity);
        s.SetDirection(lookPos + upset);
        s.SetDamage(damage);
    }
}
