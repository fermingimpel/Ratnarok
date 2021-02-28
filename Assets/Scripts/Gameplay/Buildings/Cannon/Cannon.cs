using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Structure {
    [SerializeField] Bullet shoot;
    [SerializeField] Vector3 bulletUpset;
    float timerToAttack = 0;
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
        ratAnimator.Play("Attack");
        AkSoundEngine.PostEvent("torret_stuff", this.gameObject);
        Bullet s = Instantiate(shoot, transform.position + bulletUpset, Quaternion.identity);
        s.SetDirection(lookPos + bulletUpset);
        s.SetDamage(damage);
    }
}
