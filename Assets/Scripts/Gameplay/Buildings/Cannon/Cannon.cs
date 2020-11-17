using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Building {
    [SerializeField] Bullet shoot;
    [SerializeField] Vector3 upset;

    protected override void StopDefend() {
        StopCoroutine(PrepareAttack());
        defending = false;
    }

    protected override void StartDefend() {
        StartCoroutine(PrepareAttack());
        defending = true;
    }
    IEnumerator PrepareAttack() {
        yield return new WaitForSeconds(preparationTime);
        Attack();
        yield return null;
    }
    protected override void Attack() {
        if (!defending) 
            return;

        animator.Play("Shoot");

        Bullet s = Instantiate(shoot, transform.position + upset, Quaternion.identity);
        s.SetDirection(lookPos + upset);
        s.SetDamage(damage);
        StartCoroutine(PrepareAttack());
    }


}
