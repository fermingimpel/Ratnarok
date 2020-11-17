using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : Building {
    [SerializeField] Bullet shoot;
    [SerializeField] Vector3 offset;
    protected override void Start() {
        base.Start();
    }
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
        StopCoroutine(PrepareAttack());
        Attack();
        yield return null;
    }
    protected override void Attack() {
        if (!defending)
            return;

        animator.Play("Shoot");

        Bullet s = Instantiate(shoot, transform.position + offset, shoot.transform.rotation);
        s.SetDirection(transform.forward + offset);
        s.SetDamage(damage);
        StartCoroutine(PrepareAttack());
    }
}
